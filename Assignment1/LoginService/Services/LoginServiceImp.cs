 using Data.Context;
using LoginService.Dtos;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Services
{
    public class LoginServiceImp : ILoginService
    {
        private readonly SystemContext _context;
        private readonly IConfiguration _configuration;
        private readonly float _jwtTimeOut = 30;
        public LoginServiceImp(SystemContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GetUsername(string email)
        {
            try
            {
                var account = _context.Accounts.SingleOrDefault(a => a.Email.Equals(email));
                if (account == null)
                {
                    return null;
                }
                return account.Name;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public JwtResponseDto Login(LoginAccountDto request)
        {
            if (VerifyAccount(request.Email, request.Password))
            {
                return GetJwtToken(request.Email);
            }
            else
            {
                return new JwtResponseDto
                {
                    IsSuccess = false,
                    Token = null
                };
            }

        }

        private bool VerifyAccount(string email, string password)
        {
            var account = _context.Accounts.FirstOrDefault(a => a.Email.Equals(email));
            var IsCorrectPassword = ComparePassword(account.Password, password);
            if (IsCorrectPassword)
            {
                return true;
            }
            return false;
        }

        private JwtResponseDto GetJwtToken(string email)
        {
            var tokenPrefix = "Bearer";
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Email", email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtTimeOut),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256),
                Audience = _configuration["Jwt:Audience"],
                Issuer = _configuration["Jwt:Issuer"]
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var result = tokenPrefix + " " + jwtTokenHandler.WriteToken(token);
            return new JwtResponseDto
            {
                IsSuccess = true,
                Token = result
            };
        }

        //private string GetPasswordEcrypt(string password)
        //{
        //    byte[] salt = new byte[128 / 8];

        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(salt);
        //    }
        //    var sal = Convert.ToBase64String(salt);

        //    // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        //    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
        //        password: password,
        //        salt: salt,
        //        prf: KeyDerivationPrf.HMACSHA1,
        //        iterationCount: 10000,
        //        numBytesRequested: 256 / 8));

        //    var result = hashed + "|" + sal;
        //    return result;
        //}
        private bool ComparePassword(string pass, string input)
        {
            var dbPassword = pass.Split("|");
            var realPassword = dbPassword[0];
            byte[] salt = Convert.FromBase64String(dbPassword[1]);
            string passInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            bool is_same = realPassword.Equals(passInput);
            return is_same;
        }
    }
}
