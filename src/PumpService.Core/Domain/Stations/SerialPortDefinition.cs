using PumpService.Core.Domain.Lookups;
using System.IO.Ports;

namespace PumpService.Core.Domain.Stations
{
    public partial class SerialPortDefinition : BaseDomainEntity
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        public int DataBits { get; set; }
        public Parity Parity { get; set; }
        public StopBits StopBits { get; set; }
        public int ReadTimeout { get; set; }
        public int WriteTimeout { get; set; }
        public virtual LookupTable PortType { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        public List<SelectListItem> PortTypes { get; set; }
        public List<SelectListItem> Parities { get; set; }
        public List<SelectListItem> StopBitses { get; set; }
        public long PortTypeId { get; set; }
    }
}