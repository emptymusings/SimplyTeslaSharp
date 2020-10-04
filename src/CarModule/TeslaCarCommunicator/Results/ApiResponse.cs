using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Results
{
    /// <summary>
    /// POCO designed around standard responses from the Tesla API
    /// </summary>
    internal class ApiResponse<T>
    {
        public T response { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}