using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster.Data
{
    /// <summary>
    /// POCO class for a vehicle list as returned from Tesla's servers
    /// </summary>
    internal class VehicleListData
    {
        public VehicleSummaryData[] response { get; set; }
        public int count { get; set; }
    }
}