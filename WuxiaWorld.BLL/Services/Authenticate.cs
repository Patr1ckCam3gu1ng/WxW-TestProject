namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using DAL.Models;

    using Interfaces;

    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class Authenticate : IAuthenticate {

        private const string Admin = "admin";
        private readonly JwtToken _token;

        public Authenticate(IOptions<JwtToken> jwtToken) {
            _token = jwtToken.Value;
        }

        public bool Validate(LoginModel login) {

            const string user = "user";

            return login.UserName == Admin && login.Password == Admin ||
                   login.UserName == user && login.Password == user;
        }

        public string GenerateToken(string loginUserName) {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_token.Key));

            var isAdmin = loginUserName.ToLowerInvariant() == Admin;

            var claimIdentities = new List<Claim> {new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")};

            var token = new JwtSecurityToken(_token.Issuer,
                _token.Issuer,
                claimIdentities,
                expires: DateTime.Now.AddMinutes(_token.AddMinutes),
                signingCredentials:
                new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}