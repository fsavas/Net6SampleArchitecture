using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PumpService.Core.Domain.Products;

namespace PumpService.Data.Mapping.Products
{
    public partial class ProductGroupMap : BaseEntityTypeConfiguration<ProductGroup>
    {
        #region Methods

        public override void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.ToTable(nameof(ProductGroup));
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Code).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.IsActive);
            //builder.Property(e => e.IsDeleted);

            builder.HasAlternateKey(e => e.Code);

            base.Configure(builder);
        }

        #endregion Methods
    }
}