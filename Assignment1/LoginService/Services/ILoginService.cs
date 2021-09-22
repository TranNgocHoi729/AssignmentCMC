using LoginService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Services
{
    public interface ILoginService
    {
        public JwtResponseDto Login(LoginAccountDto request);
    }
}
