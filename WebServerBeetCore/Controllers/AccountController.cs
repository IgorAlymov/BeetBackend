using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using BeetAPI.DataAccessLayer;
using BeetAPI.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebServerBeetCore.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        SocialUserRepository _db;

        public AccountController(SocialUserRepository rep)
        {
            _db = rep;
        }
        //
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]SocialUser user)
        {
            var entryUser =  _db.Get(user.Email);
            try
            {
                if (entryUser != null && entryUser.Password == user.Password)
                {
                    await Authenticate(user);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        private async Task Authenticate(SocialUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        //
        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        //
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]SocialUser user)
        {
            try
            {
                _db.Create(user);
                _db.Save();
                await Authenticate(user);
                return Ok();
            }
            catch (Exception)
            {
                return ValidationProblem();
            }
        }

    }
}
