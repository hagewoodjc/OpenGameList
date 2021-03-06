﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OpenGameList.Data;
using OpenGameList.Data.Users;
using Newtonsoft.Json;

namespace OpenGameList.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected ApplicationDbContext DbContext;
        protected SignInManager<ApplicationUser> SignInManager;
        protected UserManager<ApplicationUser> UserManager;

        ///<summary> Returns a suitable JsonSerializerSettings object that can be used to generate the JsonResult return value for this controller's methods. </summary>
        protected JsonSerializerSettings DefaultSettings
        {
            get
            {
                return new JsonSerializerSettings()
                {
                    Formatting = Formatting.Indented
                };
            }
        }

        public BaseController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            // Dependency Injection
            DbContext = context;
            SignInManager = signInManager;
            UserManager = userManager;
        }

        ///<summary Retrieves the .NET Core Identity User Id for the current ClaimsPrincipal. </summary>
        ///<returns></returns>
        public async Task<string> GetCurrentUserId()
        {
            // if the user is not authenticated, throw an exception
            if (!User.Identity.IsAuthenticated)
                throw new NotSupportedException();

            var info = await SignInManager.GetExternalLoginInfoAsync();
            if (info == null)
                // internal provider
                return User.FindFirst(ClaimTypes.NameIdentifier).Value;
            else
            {
                // external provider
                var user = await UserManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                if (user == null) throw new NotSupportedException();
                return user.Id;
            }
        }
    }
}
