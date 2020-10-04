using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Data;

namespace SimplTeslaMaster.Results.GetResults
{
    /// <summary>
    /// Class used to request a vehicle list for the user's account
    /// </summary>
    internal class VehicleListGetResult : GetResultBase<VehicleListData>
    {
        internal VehicleListGetResult() :
            base(Constants.URL_VEHICLES)
        {

        }
    }
}