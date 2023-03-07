using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Products;

namespace PumpService.Data.Mapping.Products
{
    public partial class ProductMap : BaseEntityTypeConfiguration<Product>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.UnitPrice).IsRequired();
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasOne(e => e.ProductGroup)
                .WithMany()
                .HasForeignKey(e => e.ProductGroupId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasAlternateKey(e => e.Code);

            base.Configure(builder);
        }

        #endregion Methods
    }
}