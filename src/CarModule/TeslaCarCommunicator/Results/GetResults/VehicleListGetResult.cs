using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;

namespace SimplTeslaCar.Results.GetResults
{
    /// <summary>
    /// Requests a list of vehicles from the API.  Extends BaseGetResult
    /// </summary>
    internal class VehicleListGetResult : GetResultBase<VehicleListData>
    {
        internal VehicleListGetResult() :
            base(Constants.URL_VEHICLES)
        {

        }
    }
}