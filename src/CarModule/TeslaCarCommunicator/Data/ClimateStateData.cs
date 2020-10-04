using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of Climate Data returned from Tesla's API
    /// </summary>
    internal class ClimateStateData
    {
        public decimal inside_temp { get; set; }
        public decimal outside_temp { get; set; }
        public decimal driver_temp_setting { get; set; }
        public decimal passenger_temp_setting { get; set; }
        public int left_temp_direction { get; set; }
        public int right_temp_direction { get; set; }
        public bool is_front_defroster_on { get; set; }
        public bool is_preconditioning { get; set; }
        public bool is_rear_defroster_on { get; set; }
        public int fan_status { get; set; }
        public bool is_climate_on { get; set; }
        public decimal min_avail_temp { get; set; }
        public decimal max_avail_temp { get; set; }
        public int seat_heater_left { get; set; }
        public int seat_heater_right { get; set; }
        public int seat_heater_rear_left { get; set; }
        public int seat_heater_rear_right { get; set; }
        public int seat_heater_rear_center { get; set; }
        public int seat_heater_rear_right_back { get; set; }
        public int seat_heater_rear_left_back { get; set; }
        public bool battery_heater { get; set; }
        public bool? battery_heater_no_power { get; set; }
        public bool steering_wheel_heater { get; set; }
        public bool wiper_blade_heater { get; set; }
        public bool side_mirror_heaters { get; set; }
        public bool smart_preconditioning { get; set; }
        public bool is_auto_conditioning_on { get; set; }
        public long timestamp { get; set; }
    }
}