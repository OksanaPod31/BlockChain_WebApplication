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

        private async Task<string> EntityUserName(string id)
        {
            var y = await userManager.FindByIdAsync(id);
            return y.UserName;
        }

        public string GetUserId(string name)
        {
            var t = EntityUser(name);
            return t.Result;
        }

        public string GetUserNameById(string Id)
        {
            var t = EntityUserName(Id);
            return t.Result;
        }

    }
}
