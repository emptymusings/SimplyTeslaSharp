using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using Newtonsoft.Json;

namespace SimplTeslaMaster.WebRequests
{
    internal enum CallTypes
    {
        NewAuthorization,
        RefreshAuthorization,
        Request,
        Command
    }

    /// <summary>
    /// Class that creates a default HTTPS request
    /// </summary>
    internal class HttpsRequestFactory
    {        
        /// <summary>
        /// Create the client request
        /// </summary>
        /// <param name="callType">The type of call that will be made to Tesla's servers</param>
        /// <returns></returns>
        public static HttpsClientRequest GetClientRequest(CallTypes callType)
        {
            HttpsClientRequest request = new HttpsClientRequest();
            RequestTypes requestType;

            // Set the request's end point based on the type of call being made
            SetRequestBaseUrl(request, callType);

            // Get the request type based on the call type
            requestType = GetRequestType(callType);

            try
            {
                // Build the HTTP request's content
                ContentFactory.BuildRequestContentString(request, requestType);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequests.HttpRequestFactory.GetClientRequest()::Failed to build request content string. Request type: " + 
                    requestType.ToString() + " " + ex.ToString());
            }

            try
            {
                // Build the HTTP request's headers
                HeaderFactory.BuildRequestHeaders(request, requestType);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequests.HttpRequestFactory.GetClientRequest()::Failed to create HTTP headers. Request type: " + requestType.ToString() +
                    " " + ex.ToString());
            }

            return request;
        }

        /// <summary>
        /// Gets the type of request based on the call type
        /// </summary>
        /// <param name="callType">The call type to be evaluated</param>
        private static RequestTypes GetRequestType(CallTypes callType)
        {
            RequestTypes requestType = RequestTypes.StandardGetRequest;

            switch (callType)
            {
                case CallTypes.NewAuthorization:
                    requestType = RequestTypes.NewToken;
                    break;
                case CallTypes.RefreshAuthorization:
                    requestType = RequestTypes.RefreshToken;
                    break;
                case CallTypes.Command:
                    requestType = RequestTypes.StandardPostRequest;
                    break;
                case CallTypes.Request:
                    requestType = RequestTypes.StandardGetRequest;
                    break;
            }

            return requestType;
        }

        /// <summary>
        /// Sets the requests end point URL based on the call type
        /// </summary>
        /// <param name="request">The request to which the URL will be assigned</param>
        /// <param name="callType">The type of call to be made</param>
        private static void SetRequestBaseUrl(HttpsClientRequest request, CallTypes callType)
        {
            switch (callType)
            {
                case CallTypes.NewAuthorization:
                case CallTypes.RefreshAuthorization:
                    request.Url.Parse(Constants.URL_AUTHORIZATION);
                    break;
                default:
                    request.Url.Parse(Constants.URL_BASE);
                    break;
            }
        }

    }
}