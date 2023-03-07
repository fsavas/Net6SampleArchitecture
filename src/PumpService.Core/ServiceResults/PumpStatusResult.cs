using PumpService.Core.Defaults;

namespace PumpService.Core.Results
{
    public class PumpStatusResult
    {
        public EnumClasses.DcrStates? Status { get; set; }
        public byte AbuAddress { get; set; }
        public byte CpuId { get; set; }
        public byte NozzleId { get; set; }
    }
}
