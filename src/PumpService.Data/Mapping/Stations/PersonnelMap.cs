using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Stations;

namespace PumpService.Data.Mapping.Stations
{
    public partial class PersonnelMap : BaseEntityTypeConfiguration<Personnel>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Personnel> builder)
        {
            builder.ToTable(nameof(Personnel));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PersonnelIdNumber).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.DiscountRate);
            builder.Property(e => e.CardId).IsRequired().HasMaxLength(100);
            builder.Property(e => e.NationalIdNumber).IsRequired().HasMaxLength(100);
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.PositionType)
                .WithMany()
                .HasForeignKey(e => e.PositionTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => e.PersonnelIdNumber);
            builder.HasAlternateKey(e => e.CardId);
            builder.HasAlternateKey(e => e.NationalIdNumber);

            builder.Ignore(e => e.PositionTypes);

            base.Configure(builder);
        }

        #endregion Methods
    }
}