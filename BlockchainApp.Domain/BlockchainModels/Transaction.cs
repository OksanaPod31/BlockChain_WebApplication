using BlockchainApp.Domain.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Domain.BlockchainModels
{
    public class Transaction
    {
        public long TimeStamp { get; set; }
        public string Sender { set; get; }
        public string Recipient { set; get; }
        public string DataContent { set; get; }

        public Transaction(long timeStamp, string sender, string recipient, string dataContent)
        {
            TimeStamp = timeStamp;
            Sender = sender;
            Recipient = recipient;
            DataContent = EDS.EncryptWithKey(dataContent);
        }

        public string GetContetnt()
        {
            return EDS.DecryptWithKey(DataContent);
        }
    }
}
