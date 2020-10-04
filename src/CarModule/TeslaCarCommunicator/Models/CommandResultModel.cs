using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Models
{
    /// <summary>
    /// Converts the base Tesla response to be parsed into the appropriate object type
    /// </summary>
    public class CommandResultModel
    {
        public string reason { get; set; }
        public string result { get; set; }
    }
}