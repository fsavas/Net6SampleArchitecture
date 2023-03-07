﻿using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Pumps
{
    public partial class NozzleGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleGridModel_Address_DisplayName)]
        public int Address { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleGridModel_Product_Name_DisplayName)]
        public string Product_Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Pumps_NozzleGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}