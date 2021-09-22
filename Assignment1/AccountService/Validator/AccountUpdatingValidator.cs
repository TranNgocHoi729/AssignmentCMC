using AccountService.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AccountService.Validator
{
    public class AccountUpdatingValidator : AbstractValidator<AccountAddingDto>
    {
        public AccountUpdatingValidator()
        {
            RuleFor(a => a.EmailOptIn).Must(a => checkEmailOpt(a)).WithMessage("Wrong email format");
            RuleFor(a => a.DOB).NotNull().WithMessage("Date time can not be null").NotEmpty().WithMessage("Date time can not be empty");
            RuleFor(a => a.MobileNumber).CreditCard().WithMessage("Phone number is wrong format");
            RuleFor(a => a.Name).NotNull().WithMessage("Name can not be null")
                .NotEmpty().WithMessage("Name can not be empty");
        }

        private static bool checkEmailOpt(string emailOtp)
        {
            if (string.IsNullOrEmpty(emailOtp))
            {
                return true;
            }
            else
            {
                return EmailValidate(emailOtp);
            }
        }

        public static bool EmailValidate(string email)
        {
            if (email == null)
            {
                return true;
            }
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
            {
                return true;
            }
            return false;
        }
    }
}
