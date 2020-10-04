using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;

namespace SimplTeslaCar.Results.GetResults
{
    /// <summary>
    /// Requests a summary from the API.  This does not require waking the vehicle to get information.  Extends BaseGetResult
    /// </summary>
    internal class VehicleSummaryGetResult : GetResultBase<VehicleSummaryData>
    {
        internal VehicleSummaryGetResult(string idString) :
            base(Constants.URL_VEHICLES + "/" + idString)
        {

        }
    }
}