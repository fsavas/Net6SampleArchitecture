using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Data.Mapping.Pumps
{
    public partial class PumpSalesMap : BaseEntityTypeConfiguration<PumpSales>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<PumpSales> builder)
        {
            builder.ToTable(nameof(PumpSales));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Amount).IsRequired();
            builder.Property(e => e.PumpQuantity).IsRequired();
            builder.Property(e => e.NetQuantity).IsRequired();
            builder.Property(e => e.UnitPrice).IsRequired();
            builder.Property(e => e.TransactionStartTime).IsRequired();
            builder.Property(e => e.TransactionEndTime).IsRequired();

            builder.HasOne(e => e.FillingPoint)
                .WithMany()
                .HasForeignKey(e => e.FillingPointId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Nozzle)
                .WithMany()
                .HasForeignKey(e => e.NozzleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
        }

        #endregion Methods
    }
}