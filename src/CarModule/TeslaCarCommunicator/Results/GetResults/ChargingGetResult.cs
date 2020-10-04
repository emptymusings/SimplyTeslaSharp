using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;

namespace SimplTeslaCar.Results.GetResults
{
    /// <summary>
    /// Request for Charging data from API.  Extends BaseGetResult
    /// </summary>
    internal class ChargingGetResult : GetResultBase<ApiResponse<ChargeStateData>>
    {
        internal ChargingGetResult(string idString) :
            base(Constants.URL_VEHICLES + "/" + idString + "/data_request/charge_state")
        {

        }
    }
}