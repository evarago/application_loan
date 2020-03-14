using ApplicationLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.Data.Mapping
{
    public class InterestRateMap : IEntityTypeConfiguration<InterestRate>
    {
        public void Configure(EntityTypeBuilder<InterestRate> builder)
        {
            builder.ToTable("InterestRate");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.VlInterest)
                .IsRequired()
                .HasColumnName("VlInterest");
        }
    }
}