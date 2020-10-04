using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;

namespace SimplTeslaCar.Results.PostResults
{
    /// <summary>
    /// Tells a vehicle to wake up so it can receive/respond to requests. Extends PostResultBase
    /// </summary>
    internal class WakePostResult : PostResultBase<ApiResponse<VehicleSummaryData>>
    {
        public WakePostResult(string vehicleIdString) :
            base(Constants.URL_VEHICLES + "/" + vehicleIdString + "/wake_up")
        {

        }

        public static WakePostResult RequestWake(string vehicleIdString)
        {
            WakePostResult wakeResult;
            
            wakeResult = new WakePostResult(vehicleIdString);

            wakeResult.GetResult();

            return wakeResult;
        }
    }
}