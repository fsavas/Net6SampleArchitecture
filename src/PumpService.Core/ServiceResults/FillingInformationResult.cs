using PumpService.Core.Defaults;

namespace PumpService.Core.Results
{
    public class FillingInformationResult
    {
        public double Amount { get; set; }
        public double Volume { get; set; }
        public double UnitPrice { get; set; }
        public bool IsEndOfFilling { get; set; }
    }
}
