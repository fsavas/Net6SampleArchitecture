using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Stations;

namespace PumpService.Data.Mapping.Stations
{
    public partial class SerialPortDefinitionMap : BaseEntityTypeConfiguration<SerialPortDefinition>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<SerialPortDefinition> builder)
        {
            builder.ToTable(nameof(SerialPortDefinition));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PortName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.BaudRate).IsRequired();
            builder.Property(e => e.DataBits).IsRequired();
            builder.Property(e => e.Parity).IsRequired();
            builder.Property(e => e.StopBits).IsRequired();
            builder.Property(e => e.ReadTimeout).IsRequired();
            builder.Property(e => e.WriteTimeout).IsRequired();
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.PortType)
                .WithMany()
                .HasForeignKey(e => e.PortTypeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => e.PortName);

            builder.Ignore(e => e.PortTypes);
            builder.Ignore(e => e.Parities);
            builder.Ignore(e => e.StopBitses);

            base.Configure(builder);
        }

        #endregion Methods
    }
}