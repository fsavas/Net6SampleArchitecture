using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpService.Web.Core.Models
{
    public partial class BasePumpServiceModel
    {
        public virtual int AbuAddress { get; set; }
        public virtual int CpuId { get; set; }
        //public virtual int NozzleId { get; set; }
    }
}
