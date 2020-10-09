using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Data;
using SimplTeslaMaster.Results.GetResults;

namespace SimplTeslaMaster
{
    /// <summary>
    /// Model class to expose Vehicle Summary data to the Crestron processor
    /// </summary>
    public class VehicleSummaryModel
    {
        public VehicleSummaryModel()
        {
            Data = new VehicleSummaryData();
        }

        internal VehicleSummaryModel(VehicleSummaryData data)
        {
            Data = data;
        }

        internal VehicleSummaryData Data { get; set; }

        public string Name { get { return Data.display_name; } }
        public string Status { get { return Data.state; } }
        public short InService { get { return Data.in_service ? (short)1 : (short)0; } }
        public string Id { get { return Data.id_s; } }
        public int CalendarEnabled { get { return Data.calendar_enabled ? 1 : 0; } }
        public string Vin { get { return Data.vin; } }

        public void RefreshData(string idString)
        {
            VehicleSummaryGetResult result = new VehicleSummaryGetResult(idString);
            result.GetResult();
            Data = result.Result;
        }

        public void RefreshData()
        {
            RefreshData(Id);
        }
    }
}