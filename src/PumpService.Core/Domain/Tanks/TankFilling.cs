namespace PumpService.Core.Domain.Tanks
{
    public partial class TankFilling : BaseDomainEntity
    {
        public DateTime? FillingStartTime { get; set; }//zorunlu alan
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}