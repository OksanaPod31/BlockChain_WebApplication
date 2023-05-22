﻿using BlockchainApp.Domain.BlockchainModels;
using BlockchainApp.Persistence;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Web.Shared
{
    public class ChatRoomManager
    {
        private readonly BlockchainDbContext context;
        public event Action<string> MessageSended;
        public ChatRoomManager(BlockchainDbContext context) { this.context = context; }

        public List<HelloReply> GetMessages()
        {
            
            List<HelloReply> messages = new List<HelloReply>();
            foreach (var message in context.Transactions)
            {
                messages.Add(new HelloReply() { Message = message.DataContent });
                //var reply = new HelloReply { Message = message.DataContent };
                //rep.Append(reply);
            }
           return messages;

        }

        public async Task AddMessageAsync(HelloReply message)
        {
            var messagetranc = new Transaction { DataContent = message.Message, Recipient = "a", Sender = "admin", TimeStamp = DateTime.Now };
            Blockchain.AddTransactionToPool(messagetranc);
            //await context.Transactions.AddAsync(messagetranc);
            //await context.SaveChangesAsync();

            MessageSended?.Invoke(message.Message);
        }
    }
}