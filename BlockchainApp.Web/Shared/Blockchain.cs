using BlockchainApp.Domain.Common.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Domain.BlockchainModels
{
    public class Blockchain
    {
        

        public static List<Transaction> TransactionPool = new List<Transaction>();
        public  static IList<Block> Blocks { set; get; }
        
        public void AddTransactionToPool(Transaction trx)
        {
            
            TransactionPool.Add(trx);
        }

        public Blockchain()
        {
            Initialize();
        }

        private static Block CreateGenesisBlock()
        {
            var trx = new Transaction(DateTime.Now.Ticks, "Founder", "Genesis Account", "GenesisBlock");

            var trxList = new List<Transaction> { trx }; ;
            return new Block(1, string.Empty.ConvertToBytes(), trxList, "Admin");
        }
        public void ClearPool()
        {
            TransactionPool = new List<Transaction>();
        }

        private void Initialize()
        {
            Blocks = new List<Block>
                {
                    CreateGenesisBlock()
                };
            TransactionPool = new List<Transaction>();
        }
        public void CreateBlock()
        {
            var lastBlock = GetLastBlock();
            var nextHeight = lastBlock.Height + 1;
            var prevHash = lastBlock.Hash;
            var transactions = TransactionPool;
            var block = new Block(nextHeight, prevHash, transactions, "Admin");
            Blocks.Add(block);
        }

        public void AddBlock(List<Transaction> transactions)
        {
            var lastBlock = GetLastBlock();
            var nextHeight = lastBlock.Height + 1;
            var prevHash = lastBlock.Hash;
            var timestamp = DateTime.Now.Ticks;
            var block = new Block(nextHeight, prevHash, transactions, "Admin");
            Blocks.Add(block);
        }
        public static Block GetLastBlock()
        {
            return Blocks[Blocks.Count - 1];
        }

        public void PrintTransactionHistory(string name)
        {

            Console.WriteLine("\n\n====== Transaction History for {0} =====", name);

            foreach (Block block in Blocks)
            {
                var transactions = block.Transactions;
                foreach (var transaction in transactions)
                {
                    var sender = transaction.Sender;
                    var recipient = transaction.Recipient;

                    if (name.ToLower().Equals(sender.ToLower()) || name.ToLower().Equals(recipient.ToLower()))
                    {
                        Console.WriteLine("Timestamp :{0}", transaction.TimeStamp);
                        Console.WriteLine("Sender:   :{0}", transaction.Sender);
                        Console.WriteLine("Recipient :{0}", transaction.Recipient);
                        Console.WriteLine("Message    :{0}", transaction.DataContent);
                        Console.WriteLine("--------------");

                    }
                }
            }
        }

        public void PrintLastBlock()
        {
            var lastBlock = GetLastBlock();
            PrintBlock(lastBlock);
        }

        public void PrintBlocks()
        {

            Console.WriteLine("\n\n====== Blockchain Explorer =====");

            foreach (Block block in Blocks)
            {
                PrintBlock(block);
            }

        }

        private void PrintBlock(Block block)
        {
            Console.WriteLine("PrintBlock");
            Console.WriteLine("Height      :{0}", block.Height);
            Console.WriteLine("Timestamp   :{0}", block.TimeStamp.ConvertToDateTime());
            Console.WriteLine("Prev. Hash  :{0}", block.PrevHash.ConvertToHexString());
            Console.WriteLine("Hash        :{0}", block.Hash.ConvertToHexString());
            Console.WriteLine("Transactins :{0}", block.Transactions.ConvertToString());
            Console.WriteLine("Creator     :{0}", block.Creator);
            Console.WriteLine("--------------");
        }

        //public static void BuildNewBlock()
        //{

        //    var trxPool = TransactionPool;
        //    var lastBlock = GetLastBlock();

        //    if (trxPool.Count() <= 0)
        //    {

        //        var lstTrx = new List<Transaction>();
        //        string tempTransactions = JsonConvert.SerializeObject(lstTrx);
        //        var block = new Block(lastBlock, tempTransactions);
        //        Console.WriteLine("Block w/o trx created height: {0}, timestamp: {1}", block.Height, block.TimeStamp);
        //        AddBlock(block);

        //    }
        //    else
        //    {
        //        var transactions = trxPool.FindAll();

        //        // create block from transaction pool
        //        string tempTransactions = JsonConvert.SerializeObject(transactions);

        //        var block = new Block(lastBlock, tempTransactions);
        //        Console.WriteLine("Block created height: {0}, timestamp: {1}", block.Height, block.TimeStamp);

        //        AddBlock(block);

        //        // move all record in trx pool to transactions table
        //        foreach (Transaction trx in transactions)
        //        {
        //            Transaction.Add(trx);
        //        }

        //        // clear mempool
        //        trxPool.DeleteAll();
        //    }

        //}
    }
}
