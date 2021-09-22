using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Sedding
{
    public static class SeddingData
    {
        public static void Seeding(this ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Account>().HasData(
                new Account { 
                    Email = "CMC1@gmail.com",
                    DOB = DateTime.Now,
                    Gender = Enums.Gender.Male,
                    MobileNumber = "0909090909",
                    Name = "NgokHoi",
                    Password = "f8cgamafgO7tEl6Y67qRrOK4JytFm3XIYxaHHhPwc74=|WkOOdSuKNVpzFOG9A3eGnA==",
                    EmailOptIn = null
                },
                 new Account
                 {
                     Email = "CMC2@gmail.com",
                     DOB = DateTime.Now,
                     Gender = Enums.Gender.Male,
                     MobileNumber = "0909090999",
                     Name = "NgokHoi1234",
                     Password = "f8cgamafgO7tEl6Y67qRrOK4JytFm3XIYxaHHhPwc74=|WkOOdSuKNVpzFOG9A3eGnA==",
                     EmailOptIn = null
                 }
                );
        }
    }
}
