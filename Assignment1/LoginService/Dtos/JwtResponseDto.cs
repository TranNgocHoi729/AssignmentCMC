using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginService.Dtos
{
    public class JwtResponseDto
    {
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
    }
}
