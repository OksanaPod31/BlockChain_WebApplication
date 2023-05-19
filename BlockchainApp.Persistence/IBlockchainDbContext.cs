using BlockchainApp.Domain.BlockchainModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Persistence
{
    public interface IBlockchainDbContext
    {
        DbSet<Block> Bloks { get; set; }
        DbSet<Transaction> Transactions { get; set; }
       
        //Task<int> SaveChangesAsync();
    }
}
