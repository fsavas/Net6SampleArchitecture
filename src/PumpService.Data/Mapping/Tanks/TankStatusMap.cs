using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Tanks;

namespace PumpService.Data.Mapping.Tanks
{
    public partial class TankStatusMap : BaseEntityTypeConfiguration<TankStatus>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<TankStatus> builder)
        {
            builder.ToTable(nameof(TankStatus));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FuelLevelVolume).IsRequired();
            builder.Property(e => e.FuelLevelLength).IsRequired();
            builder.Property(e => e.WaterLevelVolume).IsRequired();
            builder.Property(e => e.WaterLevelLength).IsRequired();
            builder.Property(e => e.Temperature).IsRequired();
            builder.Property(e => e.SatisMiktari);
            builder.Property(e => e.FuelLevel_LTNet).IsRequired();
            builder.Property(e => e.FuelLevel_LT_Kalibrasyon).IsRequired();
            builder.Property(e => e.StatusInfoDate).IsRequired();
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.Tank)
                .WithMany()
                .HasForeignKey(e => e.TankId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.OlcumSebebi)
                .WithMany()
                .HasForeignKey(e => e.OlcumSebebiId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.PompaSatis)
                .WithMany()
                .HasForeignKey(e => e.PompaSatisId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.TankDolum)
                .WithMany()
                .HasForeignKey(e => e.TankDolumId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(e => e.TankSeviyesiKacOlcumdurDegismiyor);
            builder.Ignore(e => e.TankSeviyesiKacOlcumdurArtiyor);
            builder.Ignore(e => e.TankSeviyesiKacOlcumdurAzaliyor);
            builder.Ignore(e => e.EnFazlaTekrarEdenOlcum);
            builder.Ignore(e => e.FuelLevelMMIntegerDifferenceWithPreviousTankStatus);
            builder.Ignore(e => e.FuelLevelMMInteger);

            base.Configure(builder);
        }

        #endregion Methods
    }
}