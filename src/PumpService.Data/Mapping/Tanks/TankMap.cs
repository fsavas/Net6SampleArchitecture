using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Tanks;

namespace PumpService.Data.Mapping.Tanks
{
    public partial class TankMap : BaseEntityTypeConfiguration<Tank>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Tank> builder)
        {
            builder.ToTable(nameof(Tank));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
            builder.Property(e => e.ProbeAddress).IsRequired();
            builder.Property(e => e.Capacity).IsRequired();
            builder.Property(e => e.Diameter).IsRequired();
            builder.Property(e => e.MeasurementPeriod).IsRequired();
            builder.Property(e => e.ProbeLength).IsRequired();
            builder.Property(e => e.TankGroupNo);
            builder.Property(e => e.GruptakiAktifTank).IsRequired();
            builder.Property(e => e.LowFuelAlarm).IsRequired();
            builder.Property(e => e.AutoFillingNotRisingConstant);
            builder.Property(e => e.IsDetectAutoFilling);
            builder.Property(e => e.FuelOffset);
            builder.Property(e => e.WaterOffset);
            builder.Property(e => e.ProbeAddressAsis);
            builder.Property(e => e.ProbeSerialNumber);
            builder.Property(e => e.ProbeSerialNumberApplied);
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.SerialPortDefinition)
                .WithMany()
                .HasForeignKey(e => e.SerialPortDefinitionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.ProbeType)
                .WithMany()
                .HasForeignKey(e => e.ProbeTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => e.Code);
            builder.HasAlternateKey(e => e.SerialPortDefinitionId);

            builder.Ignore(e => e.LastPersistedPeriyodikTankOlcum);
            builder.Ignore(e => e.NotPersistStartTime);
            builder.Ignore(e => e.EnSonOlculenTankYakitSeviyesi);
            builder.Ignore(e => e.SessionTankOlcumler);
            builder.Ignore(e => e.TankSeviyesiKacOlcumdurDegismiyor);
            builder.Ignore(e => e.NotPersistIntervalMinute);
            builder.Ignore(e => e.TankSeviyesiKacOlcumdurArtiyor);
            builder.Ignore(e => e.TankSeviyesiKacOlcumdurAzaliyor);
            builder.Ignore(e => e.AutoFillingStarted);
            builder.Ignore(e => e.Probe);

            base.Configure(builder);
        }

        #endregion Methods
    }
}