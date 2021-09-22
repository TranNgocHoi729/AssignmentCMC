using AccountService.Dtos;
using Common.ResponseDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Service
{
    public interface IAccountService
    {
        BaseResponseResult<int> CreateAccount(AccountAddingDto request);

        BaseResponseResult<int> UpdateAccount(AccountUpdatingDto request, string email);

        BaseResponseResult<AccountSelecttingDto> GetAccountByEmail(string email);
    }
}
