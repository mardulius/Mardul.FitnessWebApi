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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
            public async Task<ActionResult> Register(RegisterBindingModel registerModel)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                User user = new User { Email = registerModel.Email, UserName = registerModel.UserName };

                var result = await _userManager.CreateAsync(user, registerModel.Password);

                if (result.Succeeded)
                {

                    await _signInManager.SignInAsync(user, false);
                    return Ok();
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
            
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {



                var tokenString = GenerateJWT(loginModel);


                var response = new
                {
                    access_token = tokenString,
                    username = user.UserName
                };

                return Ok(response);
            }

            else
            {
                return BadRequest();
            }



        }

        private string GenerateJWT(LoginBindingModel userModel)
        {
            var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
          

            return  new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("Logout")]
            public async Task<ActionResult> Logout()
            {
                // удаляем аутентификационные куки
                await _signInManager.SignOutAsync();
                return Ok();
            }


        }
    } 

