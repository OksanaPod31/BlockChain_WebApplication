using BlockchainApp.Domain.BlockchainModels;
using Microsoft.EntityFrameworkCore;
using BlockchainApp.Domain.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;

namespace BlockchainApp.Persistence
{
    public class BlockchainDbContext : DbContext
    {
        public DbSet<Block> Bloks { get; set ; }
        public DbSet<Transaction> Transactions { get; set; }
        public BlockchainDbContext(DbContextOptions<BlockchainDbContext> options):base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>(builder =>
            {
                
                var bl = Block.Genesis("-");
                builder.HasKey(x => x.Height);
                builder.Property(x => x.Height).ValueGeneratedOnAdd();
                builder.HasData(bl);
            });
            modelBuilder.Entity<Transaction>(builder =>
            {
                builder.HasKey(x => x.TransactionId);
                builder.Property(x => x.TransactionId).ValueGeneratedOnAdd();
                builder.HasData(new Transaction {
                    TransactionId = 1, 
                    DataContent = "", 
                    Recipient = "All", 
                    Sender = "GenesisBlock", 
                    TimeStamp = DateTime.Now
                });
            });
        }

        
    }
}
