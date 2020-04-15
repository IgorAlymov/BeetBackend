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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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
        
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody]SocialUser user)
        {
            var entryUser =  _db.Get(user.Email);
            try
            {
                string hashedIncoming = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Password,
                    salt: entryUser.Salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                if (entryUser != null && hashedIncoming == entryUser.Password)
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
        
        [Route("SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]SocialUser user)
        {
            var userExist = _db.Get(user.Email);
            try
            {
                byte[] salt = new byte[128 / 8];
                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
                user.Password = hashed;
                user.Salt = salt;
                if (userExist == null)
                {
                    _db.Create(user);
                    _db.Save();
                    await Authenticate(user);
                    return Ok();
                }
                else
                    return BadRequest("This email already exists");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
