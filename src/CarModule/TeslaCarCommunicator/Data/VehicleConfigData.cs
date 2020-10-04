using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of Vehicle Configuration data retured from Tesla's API
    /// </summary>
    internal class VehicleConfigData
    {
        public bool can_accept_navigation_requests { get; set; }
        public bool can_actuate_trunks { get; set; }
        public string car_special_type { get; set; }
        public string car_type { get; set; }
        public string charge_port_type { get; set; }
        public bool eu_vehicle { get; set; }
        public string exterior_color { get; set; }
        public bool has_air_suspension { get; set; }
        public bool has_ludicrous_mode { get; set; }
        public bool motorized_charge_port { get; set; }
        public string perf_config { get; set; }
        public bool? plg { get; set; }
        public int rear_seat_heaters { get; set; }
        public string rear_seat_type { get; set; }
        public bool rhd { get; set; }
        public string roof_color { get; set; }
        public string seat_type { get; set; }
        public string spoiler_type { get; set; }
        public string sun_roof_installed { get; set; }
        public string third_row_seats { get; set; }
        public long timestamp { get; set; }
        public string wheel_type { get; set; }
    }
}