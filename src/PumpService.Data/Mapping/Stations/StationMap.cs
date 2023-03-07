using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Stations;

namespace PumpService.Data.Mapping.Stations
{
    public partial class StationMap : BaseEntityTypeConfiguration<Station>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.ToTable(nameof(Station));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);

            builder.HasAlternateKey(e => e.Name);

            base.Configure(builder);
        }

        #endregion Methods
    }
}