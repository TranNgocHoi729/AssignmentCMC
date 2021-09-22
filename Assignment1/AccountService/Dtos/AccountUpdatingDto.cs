using Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Dtos
{
    public class AccountUpdatingDto
    {
        public string Name { get; set; }

        public string MobileNumber { get; set; }

        public Gender Gender { get; set; }

        public DateTime DOB { get; set; }

        public string? EmailOptIn { get; set; }
    }
}
