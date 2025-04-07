using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ucode.Core.Models;

namespace Ucode.Api.Data.Mappings
{
    public class GradeMapping : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable("Grade");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Value)
                .IsRequired(true)
                .HasColumnType("DECIMAL(18,2)");

            builder.Property(x => x.CreatedAt)
                .IsRequired(true);

            builder.Property(x => x.UpdatedAt)
                .IsRequired(false);

            builder.Property(x => x.EnrollmentId)
                .IsRequired(true);
                   
            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnType("VARCHAR")
                .HasMaxLength(160);
        }
    }
}
