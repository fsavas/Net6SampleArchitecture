using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Data.Mapping.Pumps
{
    public partial class FillingPointMap : BaseEntityTypeConfiguration<FillingPoint>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<FillingPoint> builder)
        {
            builder.ToTable(nameof(FillingPoint));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasAlternateKey(e => e.Code);
            builder.HasAlternateKey(e => e.Address);

            base.Configure(builder);
        }

        #endregion Methods
    }
}