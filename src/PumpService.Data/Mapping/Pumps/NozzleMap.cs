using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Data.Mapping.Pumps
{
    public partial class NozzleMap : BaseEntityTypeConfiguration<Nozzle>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Nozzle> builder)
        {
            builder.ToTable(nameof(Nozzle));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.Product)
                .WithMany()
                .HasForeignKey(e => e.ProductId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.FillingPoint)
                .WithMany(e => e.Nozzles)
                .HasForeignKey(e => e.FillingPointId)
                .IsRequired();

            builder.HasAlternateKey(e => new { e.Address, e.FillingPointId });

            base.Configure(builder);
        }

        #endregion Methods
    }
}