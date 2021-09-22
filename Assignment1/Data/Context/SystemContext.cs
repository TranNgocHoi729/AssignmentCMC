using Data.Entities;
using Data.Sedding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigTable(modelBuilder);
            modelBuilder.Seeding();
        }

        private void ConfigTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(a =>
            {
                // Config data Type
                a.Property(a => a.Email).HasColumnType("varchar(255)").IsRequired();
                a.Property(a => a.Name).HasColumnType("nvarchar(255)").IsRequired();
                a.Property(a => a.Password).HasColumnType("varchar(255)").IsRequired();
                a.Property(a => a.MobileNumber).HasColumnType("varchar(15)").IsRequired();
                a.Property(a => a.Gender).HasColumnType("int").IsRequired();
                a.Property(a => a.EmailOptIn).HasColumnType("nvarchar(255)");

                // Config primary key
                a.HasKey(a => a.Email);

                // Config Unique Phone
                a.HasIndex(a => a.MobileNumber).IsUnique();
            });
        }

        public DbSet<Account> Accounts { get; set; }

    }
}
