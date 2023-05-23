using BlockchainApp.Persistence;
using BlockchainApp.Web.Shared;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Web.Server
{
    public class BlockJob : IJob
    {
        private readonly BlockchainDbContext _db;

        public BlockJob(BlockchainDbContext db)
        {
            _db = db;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await Blockchain.BuildNewBlock(_db);
            //return Task.CompletedTask;
            await Task.Delay(5000);
        }

        
    }
}
