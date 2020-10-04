using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of GUI Settings data retured from Tesla's API
    /// </summary>
    internal class GuiSettingsData
    {
        public string gui_distance_units { get; set; }
        public string gui_temperature_units { get; set; }
        public string gui_charge_rate_units { get; set; }
        public bool gui_24_hour_time { get; set; }
        public string gui_range_display { get; set; }
    }
}