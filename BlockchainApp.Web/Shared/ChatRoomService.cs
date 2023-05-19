using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockchainApp.Domain.BlockchainModels;
using BlockchainApp.Persistence;
using BlockchainApp.Web.Shared;
using Grpc.Core;

namespace BlockchainApp.Web.Shared
{
    public class ChatRoomService: ChatRoom.ChatRoomBase
    {
        //private readonly IBlockchainDbContext _context;

        //public ChatRoomService(IBlockchainDbContext context)
        //{
        //    _context = context;
        //}

        //public override async Task JoinChat(ChatRequest request, IServerStreamWriter<ChatMessage> responseStream, ServerCallContext context)
        //{
        //    foreach(var messagestrans in _context.Transactions)
        //    {
        //        await responseStream.WriteAsync(new ChatMessage { Message = messagestrans.DataContent });
        //    }   


        //}
        //public override async Task<ChatRequest> Send(ChatMessage request, ServerCallContext context)
        //{
        //    var messagetranc =  new Transaction { DataContent = request.Message };
        //    await _context.Transactions.AddAsync(messagetranc);
        //    return new ChatRequest();
        //}
    }
}
