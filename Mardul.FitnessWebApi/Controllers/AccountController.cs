using Mardul.FitnessWebApi.AccountBindingModel;
using Mardul.FitnessWebApi.Model;
using Mardul.FitnessWebApi.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mardul.FitnessWebApi.Controllers
{   
    [Route("api/[Controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
            public async Task<ActionResult> Register(RegisterBindingModel registerModel)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                User NewUser = new User { Email = registerModel.Email, UserName = registerModel.UserName };

                var result = await userManager.CreateAsync(NewUser, registerModel.Password);

                if (result.Succeeded)
                {
                    var tokenString = GenerateJWT(NewUser);


                    var response = new
                    {
                        accessToken = tokenString,
                        username = NewUser.UserName
                    };

                    return Ok(response);
                }

                else
                {
                    return BadRequest();
                }

            }

        [AllowAnonymous]
        [HttpPost("Login")]
            
        public async Task<ActionResult> Login(LoginBindingModel loginModel)
            
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var UserLogin = await userManager.FindByNameAsync(loginModel.UserName);
            if (UserLogin != null && await userManager.CheckPasswordAsync(UserLogin, loginModel.Password))
            {



                var tokenString = GenerateJWT(UserLogin);


                var response = new
                {
                    accessToken = tokenString,
                    username = UserLogin.UserName
                };

                return Ok(response);
            }

            else
            {
                return Unauthorized();
            }



        }

        private string GenerateJWT(User user)
        {
            var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Id),
            
        };
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        claims: claims,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
          

            return  new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Logout")]
            public async Task<ActionResult> Logout()
            {
                
                await signInManager.SignOutAsync();
                return Ok();
            }


        }
    } 

