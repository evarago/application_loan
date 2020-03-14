using ApplicationLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLoan.Infra.Data.Mapping
{
    public class TermsMap : IEntityTypeConfiguration<Terms>
    {
        public void Configure(EntityTypeBuilder<Terms> builder)
        {
            builder.ToTable("Terms");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Term)
                .IsRequired()
                .HasColumnName("Term");
        }
    }
}