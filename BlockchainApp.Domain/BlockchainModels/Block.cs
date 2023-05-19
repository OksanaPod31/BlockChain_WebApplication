using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BlockchainApp.Domain.Common.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockchainApp.Domain.BlockchainModels
{
    public class Block
    {
        
        public int Height { get; set; }
        public Int64 TimeStamp { get; set; }
        public byte[] PrevHash { get; set; }
        public byte[] Hash { get; set; }

        [ForeignKey(nameof(Transaction))]
        public int TransactionsId { get; set; }
        public Transaction Transaction { get; set; }
        public string Creator { get; set; }
        [NotMapped]
        public Transaction[] Transactions { get; set; }

        //public Block(int height, byte[] prevHash, Transaction[] transactions, string creator)
        //{
        //    Height = height;
        //    PrevHash = prevHash;
        //    TimeStamp = DateTime.Now.Ticks;
        //    Transactions = transactions;
        //    Hash = GenerateHash();
        //    Creator = creator;
        //}

        public byte[] GenerateHash()
        {
            var sha = SHA256.Create();
            byte[] timeStamp = BitConverter.GetBytes(TimeStamp);

            var transactionHash = Transactions.ConvertToByte();

            byte[] headerBytes = new byte[timeStamp.Length + PrevHash.Length + transactionHash.Length];

            Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
            Buffer.BlockCopy(PrevHash, 0, headerBytes, timeStamp.Length, PrevHash.Length);
            Buffer.BlockCopy(transactionHash, 0, headerBytes, timeStamp.Length + PrevHash.Length, transactionHash.Length);

            byte[] hash = sha.ComputeHash(headerBytes);

            return hash;


        }



    }
}
