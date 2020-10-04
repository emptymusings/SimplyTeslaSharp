using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using Newtonsoft.Json;
using SimplTeslaMaster;
using SimplTeslaMaster.WebRequests;

namespace SimplTeslaMaster.Tokens
{
    /// <summary>
    /// Static class used for auth token generation
    /// </summary>
    internal static class TokenGenerator
    {
        /// <summary>
        /// Requests an authorization token from the Tesla servers
        /// </summary>
        public static Token GetToken()
        {
            HttpsClient client;
            HttpsClientRequest request;
            HttpsClientResponse response = null;
            Token result = null;

            client = HttpsClientFactory.NewClient();

            // Create the Client request
            request = GetRequest();

            // Set the client's endpoint and finalize the HTTP header
            client.Url = request.Url;
            request.FinalizeHeader();

            try
            {
                // Send the request to Tesla's server, and get the response back
                response = client.Dispatch(request);

                // Convert the JSON to a token object
                result = (Token)JsonConvert.DeserializeObject(response.ContentString, typeof(Token));
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Tokens.TokenGenerator.GetToken()::Error in Tesla API (TokenGenerator.GetToken): Unable to get response. " + ex.ToString());
                ErrorLog.Exception("Error in Tesla API (TokenGenerator.GetToken): Unable to get response.", ex);
            }

            // Clean up
            request = null;
            client.Dispose();
            client = null;

            return result;
        }

        /// <summary>
        /// Generates a client request
        /// </summary>
        private static HttpsClientRequest GetRequest()
        {
            HttpsClientRequest result;

            // Determine if a token already exists
            if (Settings.TokenExists == false)
            {
                // No token exists, request a brand new token
                result = HttpsRequestFactory.GetClientRequest(CallTypes.NewAuthorization);
            }
            else
            {
                // We have a token, it just needs to refreshed
                result = HttpsRequestFactory.GetClientRequest(CallTypes.RefreshAuthorization);
            }

            return result;
        }

        /// <summary>
        /// Forms the Token Request API URL
        /// </summary>
        private static string GetTokenRequestUrl()
        {
            string url = Constants.URL_AUTHORIZATION;
            return url;
        }
    }
}