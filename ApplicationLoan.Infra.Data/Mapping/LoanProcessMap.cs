using ApplicationLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.Data.Mapping
{
    public class LoanProcessMap : IEntityTypeConfiguration<LoanProcess>
    {
        public void Configure(EntityTypeBuilder<LoanProcess> builder)
        {
            builder.ToTable("LoanProcess");

            builder.HasKey(c => c.Id);

            builder.HasOne(t => t.LoanRequest);
            //builder.HasOne(t => t.Status);
            //builder.HasOne(t => t.Terms);

            builder.Property(c => c.IdLoanRequest)
                .IsRequired()
                .HasColumnName("IdLoanRequest");

            builder.Property(c => c.IdStatus)
                .IsRequired()
                .HasColumnName("IdStatus");

            builder.Property(c => c.VlAmout)
                .IsRequired()
                .HasColumnName("VlAmount");

            //builder.Property(c => c.IdTerms)
            //    .IsRequired()
            //    .HasColumnName("Terms");
        }
    }
}