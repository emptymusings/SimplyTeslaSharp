using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO o Speed Limi data retured from Tesla's API
    /// </summary>
    internal class SpeedLimitModeData
    {
        public bool active { get; set; }
        public double current_limit_mph { get; set; }
        public double max_limit_mph { get; set; }
        public double min_limit_mph { get; set; }
        public bool pin_code_set { get; set; }
    }
}