using ApplicationLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.Data.Mapping
{
    public class LoanRequestMap : IEntityTypeConfiguration<LoanRequest>
    {
        public void Configure(EntityTypeBuilder<LoanRequest> builder)
        {
            builder.ToTable("LoanRequest");

            builder.HasKey(c => c.Id);

            builder.HasOne(t => t.Customer);
            builder.HasOne(t => t.Terms);

            builder.Property(c => c.IdCustomer)
                .IsRequired()
                .HasColumnName("IdCustomer");

            builder.Property(c => c.VlAmout)
                .IsRequired()
                .HasColumnName("VlAmount");

            builder.Property(c => c.VlIncome)
                .IsRequired()
                .HasColumnName("VlIncome");
        }
    }
}