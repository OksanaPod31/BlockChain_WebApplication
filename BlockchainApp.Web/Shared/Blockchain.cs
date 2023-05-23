using BlockchainApp.Domain.BlockchainModels;
using BlockchainApp.Domain.Common.Utils;
using BlockchainApp.Persistence;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Web.Shared
{
    public class Blockchain
    {
        

        //private readonly BlockchainDbContext context;

        public static List<Transaction> TransactionPool = new List<Transaction>();

        //public Blockchain(BlockchainDbContext _context)
        //{
        //    context = _context;
        //    TransactionPool = new List<Transaction>();
        //}

        public static void AddTransactionToPool(Transaction trx)
        {
            
            TransactionPool.Add(trx);
        }

        public static void ClearPool()
        {
            TransactionPool = new List<Transaction>();
        }

        public static Block GetLastBlock( BlockchainDbContext context)
        {
            return context.Bloks.OrderBy(x => x.Height).Last();
        }
        
        public static async Task BuildNewBlock(BlockchainDbContext context)
        {

            var trxPool = TransactionPool;
            var lastBlock = GetLastBlock(context);

            if (trxPool.Count() <= 0)
            {

                var lstTrx = new List<Transaction>();
                string tempTransactions = JsonConvert.SerializeObject(lstTrx);
                var block = new Block(lastBlock, tempTransactions);
                
                await context.Bloks.AddAsync(block);
                await context.SaveChangesAsync();

            }
            else
            {
               
                string tempTransactions = JsonConvert.SerializeObject(trxPool);

                var block = new Block(lastBlock, tempTransactions);


                await context.Bloks.AddAsync(block);
                

                
                foreach (Transaction trx in trxPool)
                {
                    await context.Transactions.AddAsync(trx);
                }

                TransactionPool.Clear();
                await context.SaveChangesAsync();
            }

        }


        //public Blockchain()
        //{
        //    Initialize();
        //}

        //private static Block CreateGenesisBlock()
        //{
        //    var trx = new Transaction(DateTime.Now.Ticks, "Founder", "Genesis Account", "GenesisBlock");

        //    var trxList = new List<Transaction> { trx }; ;
        //    return new Block(1, string.Empty.ConvertToBytes(), trxList, "Admin");
        //}


        //private void Initialize()
        //{
        //    Blocks = new List<Block>
        //        {
        //            CreateGenesisBlock()
        //        };
        //    TransactionPool = new List<Transaction>();
        //}
        //public void CreateBlock()
        //{
        //    var lastBlock = GetLastBlock();
        //    var nextHeight = lastBlock.Height + 1;
        //    var prevHash = lastBlock.Hash;
        //    var transactions = TransactionPool;
        //    var block = new Block(nextHeight, prevHash, transactions, "Admin");
        //    Blocks.Add(block);
        //}

        //public void AddBlock(List<Transaction> transactions)
        //{
        //    var lastBlock = GetLastBlock();
        //    var nextHeight = lastBlock.Height + 1;
        //    var prevHash = lastBlock.Hash;
        //    var timestamp = DateTime.Now.Ticks;
        //    var block = new Block(nextHeight, prevHash, transactions, "Admin");
        //    Blocks.Add(block);
        //}


        #region print unisless
        //public void PrintTransactionHistory(string name)
        //{

        //    Console.WriteLine("\n\n====== Transaction History for {0} =====", name);

        //    foreach (Block block in Blocks)
        //    {
        //        var transactions = block.Transactions;
        //        foreach (var transaction in transactions)
        //        {
        //            var sender = transaction.Sender;
        //            var recipient = transaction.Recipient;

        //            if (name.ToLower().Equals(sender.ToLower()) || name.ToLower().Equals(recipient.ToLower()))
        //            {
        //                Console.WriteLine("Timestamp :{0}", transaction.TimeStamp);
        //                Console.WriteLine("Sender:   :{0}", transaction.Sender);
        //                Console.WriteLine("Recipient :{0}", transaction.Recipient);
        //                Console.WriteLine("Message    :{0}", transaction.DataContent);
        //                Console.WriteLine("--------------");

        //            }
        //        }
        //    }
        //}

        //public void PrintLastBlock()
        //{
        //    var lastBlock = GetLastBlock();
        //    PrintBlock(lastBlock);
        //}

        //public void PrintBlocks()
        //{

        //    Console.WriteLine("\n\n====== Blockchain Explorer =====");

        //    foreach (Block block in Blocks)
        //    {
        //        PrintBlock(block);
        //    }

        //}

        //private void PrintBlock(Block block)
        //{
        //    Console.WriteLine("PrintBlock");
        //    Console.WriteLine("Height      :{0}", block.Height);
        //    Console.WriteLine("Timestamp   :{0}", block.TimeStamp.ConvertToDateTime());
        //    Console.WriteLine("Prev. Hash  :{0}", block.PrevHash.ConvertToHexString());
        //    Console.WriteLine("Hash        :{0}", block.Hash.ConvertToHexString());
        //    Console.WriteLine("Transactins :{0}", block.Transactions.ConvertToString());
        //    Console.WriteLine("Creator     :{0}", block.Creator);
        //    Console.WriteLine("--------------");
        //}
        #endregion


    }
}
