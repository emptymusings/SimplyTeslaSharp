using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;
using SimplTeslaCar.Results.PostResults;

namespace SimplTeslaCar.Results.GetResults
{
    /// <summary>
    /// Requests Vehicle Data from API.   Extends BaseGetResult
    /// </summary>
    internal class VehicleGetDetailsResult : GetResultBase<ApiResponse<VehicleDetailData>>
    {
        private string vehicleIdString;

        public VehicleGetDetailsResult(string vehicleIdString) :
            base(Constants.URL_VEHICLES + "/" + vehicleIdString + "/vehicle_data")
        {
            this.vehicleIdString = vehicleIdString;
        }

        private VehicleDetailData data;

        public VehicleDetailData Data
        {
            get { return data; }
            set { data = value; }
        }

        public override void GetResult()
        {
            WakePostResult wakeResult;

            base.GetResult();

            if (!string.IsNullOrEmpty(Result.error))
            {
                wakeResult = WakePostResult.RequestWake(vehicleIdString);
                
                base.GetResult();
            }

            Data = Result.response;
        }
    }
}