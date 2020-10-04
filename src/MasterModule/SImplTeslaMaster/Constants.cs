using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster
{
    /// <summary>
    /// Class containing URL information that does not change when communicating with Tesla's servers
    /// </summary>
    internal class Constants
    {
        internal const string URL_BASE = "https://owner-api.teslamotors.com";
        internal const string URL_AUTHORIZATION = URL_BASE + "/oauth/token";
        internal const string URL_BASE_API = URL_BASE + "/api/1";
        internal const string URL_VEHICLES = URL_BASE_API + "/vehicles";        
    }
}