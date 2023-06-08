using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainApp.Domain.UserModels
{
    public class ChatUser : IdentityUser
    {
      public string publicKey { get; set; }
      public string privateKey { get; set; }
    }
}
