using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Devices;

namespace PumpService.Data.Mapping.Devices
{
    public partial class DeviceMap : BaseEntityTypeConfiguration<Device>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable(nameof(Device));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();

            builder.HasOne(e => e.ParentDevice)
                .WithMany()
                .HasForeignKey(e => e.ParentDeviceId);

            builder.HasOne(e => e.DeviceType)
                .WithMany()
                .HasForeignKey(e => e.DeviceTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => new { e.Name, e.DeviceTypeId });

            base.Configure(builder);
        }

        #endregion Methods
    }
}