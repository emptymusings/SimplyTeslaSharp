using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Results
{
    /// <summary>
    /// POCO containing a value to be sent in a POST request form
    /// </summary>
    internal class FormValue
    {
        public FormValue() { }
        public FormValue(string field, string value)
        {
            Field = field;
            Value = value;
        }

        public string Field { get; set; }
        public string Value { get; set; }
    }
}