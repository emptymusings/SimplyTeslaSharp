using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of a Vehicle's State data retured from Tesla's API
    /// </summary>
    internal class VehicleStateData
    {
        public decimal api_version { get; set; }
        public string autopark_state { get; set; }
        public string autopark_state_v2 { get; set; }
        public string autopark_state_v3 { get; set; }
        public string autopark_style { get; set; }
        public bool calendar_supported { get; set; }
        public string car_version { get; set; }
        public int center_display_state { get; set; }
        public int df { get; set; }
        public int dr { get; set; }
        public int ft { get; set; }
        public bool locked { get; set; }
        public MediaStateData media_state { get; set; }
        public bool notifications_supported { get; set; }
        public decimal odometer { get; set; }
        public bool parsed_calendar_supported { get; set; }
        public int pf { get; set; }
        public int pr { get; set; }
        public bool remote_start { get; set; }
        public bool remote_start_supported { get; set; }
        public int rt { get; set; }
        public SoftwareUpdateData software_update { get; set; }
        public SpeedLimitModeData speed_limit_mode { get; set; }
        public decimal? sun_roof_percent_open { get; set; }
        public string sun_roof_state { get; set; }
        public double timestamp { get; set; }
        public bool valet_mode { get; set; }
        public bool valet_pin_needed { get; set; }
        public string vehicle_name { get; set; }
    }
}