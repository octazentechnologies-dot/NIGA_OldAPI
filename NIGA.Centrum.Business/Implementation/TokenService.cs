using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NIGA.Centrum.Business.Interfaces;
using NIGA.Centrum.Entity.DataModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NIGA.Centrum.Business.Services
{
    public class TokenService:ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _configuration;
        
        public TokenService(IConfiguration config)
        {
            _configuration = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
        }

        public async Task<string> CreateToken(UserMaster user, int expiryMin = 0)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim("RoleId", user.RoleId?.ToString() ?? ""),
                new Claim("FirmIds", user.FirmIds ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var defaultExpiryMinutes = 10080; // 7 days default
            var expiryMinutes = expiryMin == 0 ? defaultExpiryMinutes : expiryMin;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public Task<string> CreateReceptionStaffToken(
            int receptionStaffId,
            string userId,
            int doctorId,
            string fullName,
            string roleName,
            int? roleId,
            int? doctorUserId,
            int expiryMin = 0)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, receptionStaffId.ToString()),
                new Claim(JwtRegisteredClaimNames.NameId, receptionStaffId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim("DoctorID", doctorId.ToString()),
                new Claim("DoctorUserId", doctorUserId?.ToString() ?? string.Empty),
                new Claim("FullName", fullName),
                new Claim(ClaimTypes.Role, roleName),
                new Claim("RoleId", roleId?.ToString() ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var defaultExpiryMinutes = 10080;
            var expiryMinutes = expiryMin == 0 ? defaultExpiryMinutes : expiryMin;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}

