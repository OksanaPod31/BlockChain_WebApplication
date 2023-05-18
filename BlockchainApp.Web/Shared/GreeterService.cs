
using Grpc.Core;

using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BlockchainApp.Web.Shared
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
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
            var messages = new string[]
            {
                "Mess1",
                "Mess2",
                "Mess3"
            };
            foreach (var message in messages)
            {
                await responsStream.WriteAsync(new HelloReply()
                {
                    Message = message
                });
            }
            int index = 4;
            while(!context.CancellationToken.IsCancellationRequested)
            {
                await responsStream.WriteAsync(new HelloReply() { Message = $"Message{index++}" });
                await Task.Delay(2000, context.CancellationToken);
            }
            
        }
    }
}