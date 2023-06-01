using BlockchainApp.Domain.UserModels;
using Grpc.Core;
using BlockchainApp.Web.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BlockchainApp.Web.Server.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using System.Data;

namespace BlockchainApp.Web.Server
{
    [Authorize]
    public class AccountService : Account.AccountBase
    {
        #region Private Fields

        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ChatUser> _userManager;
        private readonly TokenParameters _tokenParameters;
        

        #endregion

        #region Constructor

        public AccountService(
            RoleManager<IdentityRole> roleManager,
            UserManager<ChatUser> userManager,
            TokenParameters tokenParameters)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _tokenParameters = tokenParameters;
            
        }

        #endregion

        #region Public Methods

        [AllowAnonymous]
        public override async Task<LoginResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            if (string.IsNullOrWhiteSpace(request.Login))
                return ErrorResponse("Login is not valid");

            ChatUser user = new()
            {
                UserName = request.Login,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                
                var userIdentity = await _userManager.FindByNameAsync(user.UserName);
                //string[] roles = { "User", "Admin" };
                var y = new GenericPrincipal(new GenericIdentity(userIdentity.UserName), new string[0]);

                context.GetHttpContext().User = y;

                //context.GetHttpContext().User.AddIdentity(new ClaimsIdentity { Name = userIdentity.UserName});

                return TokenResponse(await userIdentity.GenerateJwtToken(_tokenParameters, _roleManager, _userManager), userIdentity.UserName);
            }

            return new()
            {
                Error = new Error
                {
                    Message = result.Errors.FirstOrDefault()?.Description
                }
            };
        }

        [AllowAnonymous]
        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var user = await _userManager.FindByNameAsync(request.Login);

            if (user == null)
                return ErrorResponse("User not found");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isValidPassword)
                return ErrorResponse("Password wrong");
            //string[] roles = { "User", "Admin" };


            var y = new GenericPrincipal(new GenericIdentity(user.UserName), new string[0]);
           
            context.GetHttpContext().User = y;

            return TokenResponse(await user.GenerateJwtToken(_tokenParameters, _roleManager, _userManager), request.Login);
        }

        [AllowAnonymous]
        public override async Task<UserInfoResponse> GetUserProfile(UserInfoRequest request, ServerCallContext context)
        {
            //var y = new HttpContextAccessor().HttpContext.User;
            //var t  = new HttpContextAccessor().HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            ////var t = new HttpContextAccessor().HttpContext.User.
            //var yy = _userManager.GetUserAsync(new HttpContextAccessor().HttpContext.User);
            //var yyu = yy.Result.UserName;
            //var user1 = await _userManager.FindByNameAsync(yy.Identity.Name);
            var user = await _userManager.GetUserAsync(context.GetHttpContext().User);

            if (user == null)
                return new UserInfoResponse()
                {
                    Error = new Error
                    {
                        Message = "No access"
                    }
                };

            return new UserInfoResponse
            {
                Profile = new UserProfileInfo
                {
                    Username = user.UserName
                }
            };
        }

        #endregion

        #region Private Methods

        private LoginResponse ErrorResponse(string errorMessage) =>
            new()
            {
                Error = new Error
                {
                    Message = errorMessage
                }
            };

        private LoginResponse TokenResponse(string token, string name) =>

            new()
            {
                Info = new LoginInfo
                {
                    Token = token,
                    Username = name
                }
            };

        #endregion
    }
}
