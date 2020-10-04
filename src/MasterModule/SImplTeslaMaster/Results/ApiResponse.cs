using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster.Results
{
    /// <summary>
    /// Bsse class that receives all responses from the Tesla servers
    /// </summary>
    /// <typeparam name="T">The type of object expected to be returned</typeparam>
    internal class ApiResponse<T>
    {
        public T response { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
}