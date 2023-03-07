using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpService.Web.Core.Models.PumpServices
{
    public partial class NozzleTotalizerModel : BasePumpServiceModel
    {
        public int NozzleId { get; set; }
        public int? Divide { get; set; }
    }
}
