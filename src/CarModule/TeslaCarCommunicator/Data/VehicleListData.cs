using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of the Vehicle List data retured from Tesla's API
    /// </summary>
    internal class VehicleListData 
    {
        public VehicleSummaryData[] response { get; set; }
        public int count { get; set; }
    }
}