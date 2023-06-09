﻿using BlockchainApp.Domain.Common.Utils;
using BlockchainApp.Domain.UserModels;
using EllipticCurve;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Domain.BlockchainModels
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public DateTime TimeStamp { get; set; }

        
        public string SenderId { get; set; }
        
        [ForeignKey("SenderId")]
        public ChatUser Sender { set; get; }
        public string Recipient { set; get; }
        public string DataContent { set; get; }
        public List<Block> Blocks { set; get; }

        public Transaction(DateTime timeStamp, ChatUser sender, string recipient, string dataContent)
        {
            TimeStamp = timeStamp;
            Sender = sender;
            Recipient = recipient;
            DataContent = dataContent;
        }
        
        public Transaction() { }

        public static bool VerifySignature(PublicKey publicKey, string message, string signature)
        {
            //var byt = Converter.ConvertHexStringToByteArray(publicKeyHex);
            //var publicKey = PublicKey.fromString(byt);
            return Ecdsa.verify(message, Signature.fromBase64(signature), publicKey);
        }

        //public string GetContetnt()
        //{
        //    return EDS.DecryptWithKey(DataContent);
        //}
    }
}
