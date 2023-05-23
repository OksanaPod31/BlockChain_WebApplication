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
        public DateTime TimeStamp { get; set; }
        public string PrevHash { get; set; }
        public string Hash { get; set; }
        public string Creator { get; set; }
        public string TransactionStr { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Block() { }

        public Block(Block lastBlock, string transactions)
        {
            var lastHeight = lastBlock.Height;
            var lastHash = lastBlock.Hash;
            Height = lastHeight + 1;
            TimeStamp = DateTime.Now;
            PrevHash = lastHash;
            TransactionStr = transactions;
            string lastHash2 = lastHash.ToString();
            Hash = GetHash(TimeStamp, lastHash, transactions);
            Creator = "Admin";
        }
        public Block(int height, DateTime timestamp, string lastHash, string hash, string transactions)
        {
            Height = height;
            TimeStamp = timestamp;
            PrevHash = lastHash;
            Hash = hash;
            TransactionStr = transactions;
            Creator = "Admin";
        }

        public static string GetHash(DateTime timestamp, string lastHash, string transactions)
        {
            var strSum = timestamp + lastHash + transactions;
            byte[] sumBytes = Encoding.ASCII.GetBytes(strSum);
            byte[] hashBytes = SHA256.Create().ComputeHash(sumBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static Block Genesis(string transactions)
        {
            var ts = new DateTime(2020, 10, 24);
            var hash = GetHash(ts, "-", transactions);
            var block = new Block(1, ts, Convert.ToBase64String(Encoding.ASCII.GetBytes("-")), hash, transactions);
            return block;
        }
        public static string GetHash(long timestamp, string lastHash, string transactions)
        {
            var strSum = timestamp + lastHash + transactions;
            byte[] sumBytes = Encoding.ASCII.GetBytes(strSum);
            byte[] hashBytes = SHA256.Create().ComputeHash(sumBytes);
            return Convert.ToBase64String(hashBytes);
        }
        //public Block(int height, byte[] prevHash, List<Transaction> transactions, string creator)
        //{
        //    Height = height;
        //    PrevHash = prevHash;
        //    TimeStamp = DateTime.Now.Ticks;
        //    Transactions = transactions;
        //    Hash = GenerateHash();
        //    Creator = creator;
        //}

        //public byte[] GenerateHash()
        //{
        //    var sha = SHA256.Create();
        //    byte[] timeStamp = BitConverter.GetBytes(TimeStamp);

        //    var transactionHash = Transactions.ConvertToByte();

        //    byte[] headerBytes = new byte[timeStamp.Length + PrevHash.Length + transactionHash.Length];

        //    Buffer.BlockCopy(timeStamp, 0, headerBytes, 0, timeStamp.Length);
        //    Buffer.BlockCopy(PrevHash, 0, headerBytes, timeStamp.Length, PrevHash.Length);
        //    Buffer.BlockCopy(transactionHash, 0, headerBytes, timeStamp.Length + PrevHash.Length, transactionHash.Length);

        //    byte[] hash = sha.ComputeHash(headerBytes);

        //    return hash;


        //}





    }
}
