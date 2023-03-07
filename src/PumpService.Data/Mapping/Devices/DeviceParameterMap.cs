using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Devices;

namespace PumpService.Data.Mapping.Devices
{
    public partial class DeviceParameterMap : BaseEntityTypeConfiguration<DeviceParameter>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<DeviceParameter> builder)
        {
            builder.ToTable(nameof(DeviceParameter));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Value).IsRequired();

            builder.HasOne(e => e.Name)
                .WithMany()
                .HasForeignKey(e => e.NameId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Device)
                .WithMany(e => e.DeviceParameters)
                .HasForeignKey(e => e.DeviceId)
                .IsRequired();

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => new { e.NameId, e.DeviceId });

            base.Configure(builder);
        }

        #endregion Methods
    }
}