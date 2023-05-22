using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Web.Shared
{
    public class BlockJob : IInvocable
    {
        public Task Invoke()
        {
            Blockchain.BuildNewBlock();
            return Task.CompletedTask;
        }
    }
}
