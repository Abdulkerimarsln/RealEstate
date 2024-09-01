﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealEstate_Dapper_Api.Tools
{
    public class JwtTokenGenerator
    {
        public static TokenResponseViewModel GenerateToken(GetCheckAppUserViewModel model)
        {
            var claims=new List<Claim>();
            if(!string.IsNullOrWhiteSpace(model.Role))
                claims.Add(new Claim(ClaimTypes.Role, model.Role));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, model.ID.ToString()));
            if(!string.IsNullOrWhiteSpace(model.UserName))
                claims.Add(new Claim("Username", model.UserName));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefault.Key));
            var signinCredentials =new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDate = DateTime.UtcNow.AddDays(JwtTokenDefault.Expire);
            JwtSecurityToken token = new JwtSecurityToken(issuer: JwtTokenDefault.ValidIssuer, audience:JwtTokenDefault.ValidIssuer,claims:claims,notBefore:DateTime.UtcNow,expires:expireDate, signingCredentials: signinCredentials);
            JwtSecurityTokenHandler tokenHandler= new JwtSecurityTokenHandler();
            return new TokenResponseViewModel(tokenHandler.WriteToken(token),expireDate);
        }
    }
}
