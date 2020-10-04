using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster.Data
{
    /// <summary>
    /// POCO list for a vehicle summary from Tesla's servers
    /// </summary>
    internal class VehicleSummaryData
    {
        public long id { get; set; }
        public long vehicle_id { get; set; }
        public string vin { get; set; }
        public string display_name { get; set; }
        public string option_codes { get; set; }
        public string color { get; set; }
        public string[] tokens { get; set; }
        public string state { get; set; }
        public bool in_service { get; set; }
        public string id_s { get; set; }
        public bool calendar_enabled { get; set; }
        public int api_version { get; set; }
        public string backseat_token { get; set; }
        public string backseat_token_updated_at { get; set; }
    }
}