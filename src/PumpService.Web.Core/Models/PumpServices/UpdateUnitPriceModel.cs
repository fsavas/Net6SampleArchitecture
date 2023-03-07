using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpService.Web.Core.Models.PumpServices
{
    public partial class UpdateUnitPriceModel
    {
        public int AbuAddress { get; set; }
        public int CpuId { get; set; }
        public IList<NozzleIdUnitPriceModel> NozzleIdPrices { get; set; }
    }
}
