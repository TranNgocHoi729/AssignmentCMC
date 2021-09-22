using AccountService.Dtos;
using Common.ResponseDtos;
using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Service
{
    public class AccountServiceImp : IAccountService
    {

        private readonly SystemContext _context;


        public AccountServiceImp(SystemContext context)
        {
            _context = context;
        }
        public BaseResponseResult<int> CreateAccount(AccountAddingDto request)
        {
            var newAccount = new Account
            {
                Email = request.Email,
                Gender = request.Gender,
                DOB = request.DOB,
                MobileNumber = request.MobileNumber,
                Name = request.Name,
                Password = GetPasswordEcrypt(request.Password),
                EmailOptIn = request.EmailOptIn
            };
            try
            {
                _context.Accounts.Add(newAccount);
                var check = _context.SaveChanges();
                return new BaseResponseResult<int>
                {
                    Result = check,
                    IsSuccess = true
                };
            }catch(Exception e)
            {
                return new BaseResponseResult<int>
                {
                    IsSuccess = false,
                    Message = e.Message,
                    Result = 0
                };
            }
        }

        public BaseResponseResult<AccountSelecttingDto> GetAccountByEmail(string email)
        {
            try
            {
                var temp = _context.Accounts.FirstOrDefault(a => a.Email.Equals(email));
                var result = new AccountSelecttingDto
                {
                    Email = temp.Email,
                    DOB = temp.DOB,
                    Gender = temp.Gender,
                    MobileNumber = temp.MobileNumber,
                    Name = temp.Name,
                    EmailOptIn = temp.EmailOptIn
                };
                return new BaseResponseResult<AccountSelecttingDto>
                {
                    IsSuccess = true,
                    Result = result
                };
            }catch(Exception e)
            {
                return new BaseResponseResult<AccountSelecttingDto>
                {
                    IsSuccess = false,
                    Message = e.Message
                };
            }
        }

        public BaseResponseResult<int> UpdateAccount(AccountUpdatingDto request, string email)
        {
            try
            {
                var temp = _context.Accounts.FirstOrDefault(a => a.Email.Equals(email));
                temp.Name = request.Name;
                temp.Gender = request.Gender;
                temp.DOB = request.DOB;
                temp.EmailOptIn = request.EmailOptIn;
                temp.MobileNumber = request.MobileNumber;

                _context.Accounts.Update(temp);
                int result = _context.SaveChanges();
                if(result > 0)
                {
                    return new BaseResponseResult<int>
                    {
                        IsSuccess = true,
                        Result = result
                    };
                }
                else
                {
                    return new BaseResponseResult<int>
                    {
                        IsSuccess = false,
                        Result = result
                    };
                }
            }
            catch (Exception e)
            {
                return new BaseResponseResult<int>
                {
                    IsSuccess = false,
                    Result = 0,
                    Message = e.Message
                };
            }
        }

        private string GetPasswordEcrypt(string password)
        {
            byte[] salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            var sal = Convert.ToBase64String(salt);

            // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            var result = hashed + "|" + sal;
            return result;
        }
    }
}
