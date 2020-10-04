using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using Newtonsoft.Json;
using SimplTeslaCar;
using SimplTeslaCar.WebRequests;

namespace SimplTeslaCar.Tokens
{
    internal static class TokenGenerator
    {
        /// <summary>
        /// Retrieves a token from Tesla's authorization API
        /// </summary>
        public static Token GetToken()
        {
            HttpsClient client;                             
            HttpsClientRequest request;                     
            HttpsClientResponse response = null;
            Token result = null;

            // Create a new web client
            client = HttpsClientFactory.NewClient();

            // Create the reqeuest object
            request = GetRequest();

            // Set the URL to communicate with
            client.Url = request.Url;

            // Finalize the HTTP request header that was geerated in GetRequest()
            request.FinalizeHeader();


            try
            {
                // Send the request to Tesla's server
                response = client.Dispatch(request);
                
                // Covert Tesla's respose to a Token object by deserializing the JSON response
                result = (Token)JsonConvert.DeserializeObject(response.ContentString, typeof(Token));
            }
            catch (Exception ex)
            {
                // If there's a error, make note of it in the Crestron console.  This will appear both in the Toolbox's log, and in the SSH/Command line if logged in
                CrestronConsole.PrintLine("SimplTeslaCar.Tokens.TokenGenerator.GetToken()::Error in Tesla API (TokenGenerator.GetToken): Unable to get response. " + ex.ToString());
                ErrorLog.Exception("SimplTeslaCar.Tokens.TokenGenerator.GetToken()::Error in Tesla API (TokenGenerator.GetToken): Unable to get response.", ex);
            }

            // Clear the objects that were used from memory
            request = null;
            client.Dispose();
            client = null;

            return result;
        }

        /// <summary>
        /// Creates the HttpsRequest object for Tesla's authentication token servers
        /// </summary>
        private static HttpsClientRequest GetRequest()
        {
            HttpsClientRequest result;

            // Check if a Token already exists
            if (Settings.TokenExists == false)
            {
                // A token DOES NOT already exist.  Set up the request to generate a new one.
                result = HttpsRequestFactory.GetClientRequest(CallTypes.NewAuthorization);
            }
            else
            {
                // A token already exists, use the refresh_token property to request a new one
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