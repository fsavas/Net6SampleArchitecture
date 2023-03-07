namespace PumpService.Core.Domain.Products
{
    public partial class ProductGroup : BaseDomainEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}