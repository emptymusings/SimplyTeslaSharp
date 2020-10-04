using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using SimplTeslaCar.Tokens;

namespace SimplTeslaCar.WebRequests
{
    /// <summary>
    /// Enumerates available types of API requests
    /// </summary>
    internal enum RequestTypes
    {
        RefreshToken,
        StandardGetRequest,
        StandardPostRequest
    }

    internal class HeaderFactory
    {
        public static string FormBoundary = "----WebKitFormBoundary7MA4YWxkTrZu0gW";                    // Define the form boundary.  This could be a number of values, this was copied from
                                                                                                        // the POSTMAN value when sending the requst
        

        /// <summary>
        /// Creates the header for an HTTPS request
        /// </summary>
        /// <param name="request">The request to which the header will be assigned</param>
        /// <param name="requestType">The type of request to be created</param>
        public static void BuildRequestHeaders(HttpsClientRequest request, RequestTypes requestType)
        {
            AddSharedHeaders(request);                  // Adds the headers that will ALWAYS be used when communicating with Tesla's servers

            // Determine the type of request that this is
            switch (requestType)
            {
                case RequestTypes.RefreshToken:                         // If requesting a new token, set up the form for submission
                    AddFormHeaders(request);
                    break;
                default:
                    AddStandardRequestHeader(request, requestType);     // Otherwise, set the request
                    break;
            }
            
        }

        /// <summary>
        /// Creates the HTTPS request headers for all calls to Tesla's servers
        /// </summary>
        /// <param name="request">The HTTPS request object to which the headers will be assigned</param>
        private static void AddSharedHeaders(HttpsClientRequest request)
        {
            request.Header.AddHeader(new HttpsHeader("cache-control", "no-cache"));                                         // Set for no-cache
            request.Header.AddHeader(new HttpsHeader(                                                                       // Set user agent, otherwise API responds weirdly
                "User-Agent", "Mozilla/5.0 (Linux; Android 9.0.0; VS985 4G Build/LRX21Y; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/58.0.3029.83 Mobile Safari/537.36"
                )
            );
        }

        /// <summary>
        /// Defines the headers for an HTTPS form POST
        /// </summary>
        /// <param name="request">The HTTPS request to which the headers will be assigned</param>
        public static void AddFormHeaders(HttpsClientRequest request)
        {
            request.Header.AddHeader(new HttpsHeader("Content-Type", "multipart/form-data; boundary=" + FormBoundary));     // Set for form data
            

            request.RequestType = RequestType.Post;     // This is a form, so it will need to use the POST method

        }

        /// <summary>
        /// Adds the header that all requests (aside from oauth) will use
        /// </summary>
        /// <param name="request">The HTTPS request</param>
        /// <param name="headerType">The type of reqeust</param>
        private static void AddStandardRequestHeader(HttpsClientRequest request, RequestTypes headerType)
        {
            // Add the authorization token to the header
            request.Header.AddHeader(new HttpsHeader("Authorization",
                "Bearer " + Settings.Token.access_token));

            // Set the request type (POST/GET)
            if (headerType == RequestTypes.StandardGetRequest)
                request.RequestType = RequestType.Get;
            else
                request.RequestType = RequestType.Post;
        }
    }
}