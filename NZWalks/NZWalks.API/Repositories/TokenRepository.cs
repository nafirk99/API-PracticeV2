﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration configuration;

        public TokenRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create Claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));  // Claim type= email, value= user.Email

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));  // Claim type= role, value= role // In the claims collection we add a claim for each role the user has
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])); // Creating a new instance of SymmetricSecurityKey and passing the key from the configuration
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); // Creating a new instance of SigningCredentials and passing the key and the algorithm

            // Create Token
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            
            // Return Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
