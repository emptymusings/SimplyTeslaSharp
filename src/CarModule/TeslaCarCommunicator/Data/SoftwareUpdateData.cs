using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of Software Update data retured from Tesla's API
    /// </summary>
    internal class SoftwareUpdateData
    {
        public int expected_duration_sec { get; set; }
        public string status { get; set; }
    }
}