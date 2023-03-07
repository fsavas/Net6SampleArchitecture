namespace PumpService.Core.Domain.Pumps
{
    public partial class FillingPoint : BaseDomainEntity
    {
        private ICollection<Nozzle> _nozzles;

        public string Code { get; set; }
        public int Address { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }

        #region Properties

        public virtual ICollection<Nozzle> Nozzles
        {
            get => _nozzles ?? (_nozzles = new List<Nozzle>());
            protected set => _nozzles = value;
        }

        #endregion Properties
    }
}