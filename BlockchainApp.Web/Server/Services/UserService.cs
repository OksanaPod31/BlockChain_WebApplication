using BlockchainApp.Domain.UserModels;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace BlockchainApp.Web.Server.Services
{
    public  class UserService
    {
        private readonly UserManager<ChatUser> userManager;

        public UserService(UserManager<ChatUser> userManager) => this.userManager = userManager;
        private async Task<string> EntityUser(string name)
        {
            var y = await userManager.FindByNameAsync(name);
            return y.Id;
        }

        public string GetUserId(string name)
        {
            var t = EntityUser(name);
            return t.Result;
        }

    }
}
