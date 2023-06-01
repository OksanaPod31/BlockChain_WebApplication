
using BlockchainApp.Domain.BlockchainModels;
using BlockchainApp.Persistence;
using Grpc.Core;
using BlockchainApp.Web.Shared;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BlockchainApp.Domain.UserModels;
using BlockchainApp.Web.Server.Services;

namespace BlockchainApp.Web.Shared
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly ChatRoomManager _chatRoomManager;
        private List<IServerStreamWriter<HelloReply>> _listeners = new List<IServerStreamWriter<HelloReply>>();
        private readonly UserManager<ChatUser> userManager;
        private readonly UserService userService;
        //private readonly BlockchainDbContext _context;
        //private readonly IBlockchainDbContext _context;


        public GreeterService(ILogger<GreeterService> logger, ChatRoomManager context, UserManager<ChatUser> userManager, 
            UserService userService)
        {
            _logger = logger;
            _chatRoomManager = context;
            _chatRoomManager.MessageSended += ChatRoomService_MessageSended;
            this.userManager = userManager;
            this.userService = userService;
        }

        private void ChatRoomService_MessageSended(string message)
        {
            foreach(var listener in _listeners)
            {
                listener.WriteAsync(new HelloReply { Message = message });
            }
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public override async Task JoinChat(HelloRequest request, 
            IServerStreamWriter<HelloReply> responsStream, ServerCallContext context)
        {
            foreach (var messagestrans in _chatRoomManager.GetMessages())
            {
                await responsStream.WriteAsync(new HelloReply { Message = messagestrans.Message });
            }
            _listeners.Add(responsStream);
            while (!context.CancellationToken.IsCancellationRequested)
            {
                await Task.Delay(100);
                //string message;
                //await responsStream.WriteAsync(new HelloReply() { Message =  message});
                
            }
            _listeners.Remove(responsStream);

            //var tr = _context.Transactions.FirstOrDefault(x => x.TransactionId == 1).Sender;

            //var messages = new string[]
            //{
            //    $"Mess from {tr}",
            //    "Mess2",
            //    "Mess3"
            //};
            //foreach (var message in messages)
            //{
            //    await responsStream.WriteAsync(new HelloReply()
            //    {
            //        Message = message
            //    });
            //}
            //int index = 4;
            //while(!context.CancellationToken.IsCancellationRequested)
            //{
            //    await responsStream.WriteAsync(new HelloReply() { Message = $"Message{index++}" });
            //    await Task.Delay(2000, context.CancellationToken);
            //}

        }
        public override async Task<AnsRequest> Send(HelloReply request, ServerCallContext context)
        {
            //var yy = context.GetHttpContext().User;
            //var tt = userManager.GetUserAsync(yy);
            var userId = userService.GetUserId(request.Sender);
            await _chatRoomManager.AddMessageAsync(request, userId);
            //var messagetranc = new Transaction { DataContent = request.Message, Recipient="a", Sender="admin", TimeStamp= DateTime.Now.Ticks };
            //await _context.Transactions.AddAsync(messagetranc);
            //await _context.SaveChangesAsync();
            return new AnsRequest() ;

        }
        //public override async Task<AnsRequest> Send(HelloReply request, ServerCallContext context)
        //{
        //    var messagetranc = new Transaction { DataContent = request.Message };
        //    await _context.Transactions.AddAsync(messagetranc);
        //    return new ChatRequest();
        //    //return base.Send(request, context);
        //}
    }
}