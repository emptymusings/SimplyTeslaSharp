using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of Drive State data retured from Tesla's API
    /// </summary>
    internal class DriveStateData
    {
        public string shift_state { get; set; }
        public decimal? speed { get; set; }
        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
        public decimal native_latitude { get; set; }
        public bool native_location_supported { get; set; }
        public decimal native_longitude { get; set; }
        public string native_type { get; set; }
        public int heading { get; set; }
        public long gps_as_of { get; set; }
        public int power { get; set; }
    }
}