using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO of Media State data retured from Tesla's API
    /// </summary>
    internal class MediaStateData
    {
        public bool remote_control_enabled { get; set; }
    }
}