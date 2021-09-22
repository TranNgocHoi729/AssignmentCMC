using AccountService.Dtos;
using AccountService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Controllers
{
    [Route("system/account")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> CreteAccount(AccountAddingDto request)
        {
            if(request != null)
            {
                var result = _accountService.CreateAccount(request);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        
        [HttpGet("select")]
        public async Task<IActionResult> GetDetail()
        {
            var email = User.FindFirst("Email").Value;

            if (!string.IsNullOrEmpty(email))
            {
                var result = _accountService.GetAccountByEmail(email);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccount(AccountUpdatingDto request)
        {            
            if ( request != null)
            {
                var email = User.FindFirst("Email").Value;
                var result = _accountService.UpdateAccount(request, email);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest();
        }
    }
}
