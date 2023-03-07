using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Tanks;

namespace PumpService.Data.Mapping.Tanks
{
    public partial class TankFillingMap : BaseEntityTypeConfiguration<TankFilling>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<TankFilling> builder)
        {
            builder.ToTable(nameof(TankFilling));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FillingStartTime).IsRequired();
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            base.Configure(builder);
        }

        #endregion Methods
    }
}