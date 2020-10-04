using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;

namespace SimplTeslaCar.WebRequests
{
    /// <summary>
    /// Factory to create a new HTTPS client
    /// </summary>
    internal class HttpsClientFactory
    {
        public static HttpsClient NewClient()
        {
            HttpsClient result = new HttpsClient();
            
            result.KeepAlive = false;
            result.PeerVerification = false;

            return result;
        }
    }
}