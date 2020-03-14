using ApplicationLoan.Domain.Entities;
using ApplicationLoan.Infra.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.Data.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Customer> Customer { get; set; }
        public DbSet<LoanRequest> LoanRequest { get; set; }
        public DbSet<LoanProcess> LoanProcess { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Terms> Terms { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<InterestRate> InterestRate { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseMySql("Server=digital-documento.cluster-zzz.us-east-1.rds.amazonaws.com;Database=credito;Uid=zzz;Pwd=zzzz;CharSet=utf8;")
                    .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(new CustomerMap().Configure);
            modelBuilder.Entity<LoanRequest>(new LoanRequestMap().Configure);
            modelBuilder.Entity<LoanProcess>(new LoanProcessMap().Configure);
            modelBuilder.Entity<User>(new UserMap().Configure);
            modelBuilder.Entity<Status>(new StatustMap().Configure);
            modelBuilder.Entity<Terms>(new TermsMap().Configure);
            modelBuilder.Entity<InterestRate>(new InterestRateMap().Configure);
        }
    }
}