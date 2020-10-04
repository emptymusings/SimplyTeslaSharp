using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Data;

namespace SimplTeslaMaster.Results.GetResults
{
    /// <summary>
    /// Class used to request a vehicle's summary information for a specific vehicle
    /// </summary>
    internal class VehicleSummaryGetResult : GetResultBase<VehicleSummaryData>
    {
        internal VehicleSummaryGetResult(string idString) :
            base(Constants.URL_VEHICLES + "/" + idString)
        {

        }
    }
}