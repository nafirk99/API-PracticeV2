using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public readonly ITokenRepository TokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            TokenRepository = tokenRepository;
        }

        // POST: api/auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (identityResult.Succeeded)
            {
                // Add Roles to This User
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User Was Registered! Please LogIn");
                    }
                }
            }
            return BadRequest("Something Went Wrong");
        }
        //// POST: api/auth/Login
        //[HttpPost]
        //[Route("Login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        //{
        //    var user = await userManager.FindByNameAsync(loginRequestDTO.Username);
        //    if (user != null && await userManager.CheckPasswordAsync(user, loginRequestDTO.Password))
        //    {
        //        // Generate JWT Token (Assuming you have a method to generate JWT)
        //        var token = GenerateJwtToken(user);
        //        return Ok(new { Token = token });
        //    }
        //    return Unauthorized("Invalid Username or Password");
        //}

        //POST: api/auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {
                    // Get Roles For This User
                    var roles = await userManager.GetRolesAsync(user);
                   
                    if(roles != null)
                    {
                        // create token
                        var jwtToken = TokenRepository.CreateJWTToken(user, roles.ToList());
                        var responce = new LoginResponseDto
                        {
                            jwtToken = jwtToken
                        };
                        return Ok(responce);
                    }
                }
            }
            return BadRequest("Invalid Username or Password");
        }
    }
}
