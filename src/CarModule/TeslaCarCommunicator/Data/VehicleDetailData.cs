using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of Vehicle Detail data retured from Tesla's API
    /// </summary>
    internal class VehicleDetailData : VehicleSummaryData
    {
        public ChargeStateData charge_state { get; set; }
        public ClimateStateData climate_state { get; set; }
        public DriveStateData drive_state { get; set; }
        public GuiSettingsData gui_settings { get; set; }
        public SpeedLimitModeData speed_limit_mode { get; set; }
        public VehicleStateData vehicle_state { get; set; }
        public VehicleConfigData vehicle_config { get; set; }
    }
}