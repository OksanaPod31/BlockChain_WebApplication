using BlockchainApp.Domain.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Domain.BlockchainModels
{
    public class Blockchain
    {
        /// <summary>
        /// Транзакции
        /// </summary>
        public List<Transaction> TransactionPool = new List<Transaction>();
        public IList<Block> Blocks { set; get; }
        /// <summary>
        /// Добавление транзакции
        /// </summary>
        /// <param name="trx"></param>
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
            return new Block(1, string.Empty.ConvertToBytes(), trxList.ToArray(), "Admin");
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
            var block = new Block(nextHeight, prevHash, transactions.ToArray(), "Admin");
            Blocks.Add(block);
        }

        public void AddBlock(Transaction[] transactions)
        {
            var lastBlock = GetLastBlock();
            var nextHeight = lastBlock.Height + 1;
            var prevHash = lastBlock.Hash;
            var timestamp = DateTime.Now.Ticks;
            var block = new Block(nextHeight, prevHash, transactions, "Admin");
            Blocks.Add(block);
        }
        public Block GetLastBlock()
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
                        Console.WriteLine("Message    :{0}", transaction.GetContetnt());
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
    }
}
