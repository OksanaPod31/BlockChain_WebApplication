using BlockchainApp.Domain.BlockchainModels;
using Microsoft.EntityFrameworkCore;
using BlockchainApp.Domain.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.X509;
using BlockchainApp.Domain.UserModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlockchainApp.Persistence
{
    public class BlockchainDbContext : IdentityDbContext<ChatUser>
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
                    DataContent = "генезис блок", 
                    Recipient = "All", 
                    SenderId = "1",
                  
                    TimeStamp = DateTime.Now
                });
            });
            modelBuilder.Entity<ChatUser>(builder =>
            {
                builder.HasKey(x => x.Id);
                builder.HasData(new ChatUser
                {
                    Id = "1",
                    UserName = "Genezis",
                    privateKey = "undefined",
                    publicKey = "undefined"

                });
            });
            base.OnModelCreating(modelBuilder);
        }

        
    }
}
