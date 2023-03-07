namespace PumpService.Core.Domain.Products
{
    public partial class Product : BaseDomainEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public virtual ProductGroup ProductGroup { get; set; }//todo model vb. ekle
        public long ProductGroupId { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}