using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Devices;

namespace PumpService.Data.Mapping.Devices
{
    public partial class DeviceTypeMap : BaseEntityTypeConfiguration<DeviceType>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.ToTable(nameof(DeviceType));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();

            builder.HasOne(e => e.Type)
                .WithMany()
                .HasForeignKey(e => e.TypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Group)
                .WithMany()
                .HasForeignKey(e => e.GroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => new { e.TypeId, e.GroupId });

            base.Configure(builder);
        }

        #endregion Methods
    }
}