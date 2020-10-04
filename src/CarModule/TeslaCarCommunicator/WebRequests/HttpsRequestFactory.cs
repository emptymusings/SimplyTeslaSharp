using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using Newtonsoft.Json;
using SimplTeslaCar;

namespace SimplTeslaCar.WebRequests
{
    /// <summary>
    /// Enumerator for the different types of available reqeusts
    /// </summary>
    internal enum CallTypes
    {
        NewAuthorization,
        RefreshAuthorization,
        Request,
        Command
    }

    /// <summary>
    /// Factory to create an HTTPS request object
    /// </summary>
    internal class HttpsRequestFactory
    {
        /// <summary>
        /// Creates a client for an HTTPS reqeust
        /// </summary>
        /// <param name="callType">The type of call to make</param>
        public static HttpsClientRequest GetClientRequest(CallTypes callType)
        {
            HttpsClientRequest request = new HttpsClientRequest();
            RequestTypes requestType;

            // Set the base URL by the call type
            SetRequestBaseUrl(request, callType);

            // Determine the request type to send to the Content and Header factories
            requestType = GetRequestType(callType);

            // Get the content to be assigned to theheader
            ContentFactory.BuildRequestContentString(request, requestType);

            // Get the HTTP header for the request
            HeaderFactory.BuildRequestHeaders(request, requestType);

            return request;            
        }

        /// <summary>
        /// Gets the request type, based on the call type
        /// </summary>
        /// <param name="callType">The call type to be evaluated</param>
        private static RequestTypes GetRequestType(CallTypes callType)
        {
            RequestTypes requestType = RequestTypes.StandardGetRequest;

            switch (callType)
            {
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
        /// Gets the Base URL for the request, based on the call type
        /// </summary>
        /// <param name="request"></param>
        /// <param name="callType"></param>
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