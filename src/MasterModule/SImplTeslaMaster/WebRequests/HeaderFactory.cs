using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using SimplTeslaMaster.Tokens;

namespace SimplTeslaMaster.WebRequests
{

    internal enum RequestTypes
    {
        NewToken,
        RefreshToken,
        StandardGetRequest,
        StandardPostRequest
    }

    /// <summary>
    /// Class used to create the header of an HTTP request
    /// </summary>
    internal class HeaderFactory
    {
        public const string FORM_BOUNDARY = "----WebKitFormBoundary7MA4YWxkTrZu0gW";        // Sure, this could be a large number of values, but it's nice and easy to copy/paste
                                                                                            // the boundary created by POSTMAN and assign it to a constant

        /// <summary>
        /// Creates the header for an HTTP request
        /// </summary>
        /// <param name="request">The request to which the header will be assigned</param>
        /// <param name="requestType">The type of request being made</param>
        public static void BuildRequestHeaders(HttpsClientRequest request, RequestTypes requestType)
        {
            try
            {
                AddSharedHeaders(request);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequest.HeaderFactory.BuildRequestHeaders()::Failed to add shared headers. Request type: " + requestType.ToString() +
                    " " + ex.ToString());
            }

            switch (requestType)
            {
                case RequestTypes.NewToken:
                case RequestTypes.RefreshToken:
                    AddFormHeaders(request);
                    break;
                default:
                    AddStandardRequestHeader(request, requestType);
                    break;
            }

        }

        /// <summary>
        /// All requests will contain a few headers, assign then
        /// </summary>
        /// <param name="request">The request to which the headers will be assigned</param>
        private static void AddSharedHeaders(HttpsClientRequest request)
        {
            request.Header.AddHeader(new HttpsHeader("cache-control", "no-cache"));                                         // Set for no-cache
            request.Header.AddHeader(new HttpsHeader(                                                                       // Set user agent, otherwise API responds weirdly
                "User-Agent", "Mozilla/5.0 (Linux; Android 9.0.0; VS985 4G Build/LRX21Y; wv) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/58.0.3029.83 Mobile Safari/537.36"
                )
            );
        }

        /// <summary>
        /// When a form is being sent, the content type needs to be set for form-data, and the form's boundary needs to be assigned
        /// </summary>
        /// <param name="request">The request to which to assign the header</param>
        public static void AddFormHeaders(HttpsClientRequest request)
        {
            try
            {
                request.Header.AddHeader(new HttpsHeader("Content-Type", "multipart/form-data; boundary=" + FORM_BOUNDARY));     // Set for form data

                request.RequestType = RequestType.Post;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequests.HeaderFactory.AddFormHeaders()::Failed to add form headers " + ex.ToString());
            }
        }

        /// <summary>
        /// For all requests except for auth token generation, the authorization header needs to be added.  Also, we need to let the server know if 
        /// we're performing a GET or a POST request
        /// </summary>
        /// <param name="request">The request to which the headers will be assigned</param>
        /// <param name="headerType">The type of request (GET/POST) we'll be making</param>
        private static void AddStandardRequestHeader(HttpsClientRequest request, RequestTypes headerType)
        {
            try
            {
                // Add the authorization header
                request.Header.AddHeader(new HttpsHeader("Authorization",
                    "Bearer " + Settings.Token.access_token));

                // Add the GET/POST values based on the type of request that will be sent
                if (headerType == RequestTypes.StandardGetRequest)
                    request.RequestType = RequestType.Get;
                else
                    request.RequestType = RequestType.Post;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequests.HeaderFactory.AddStandardRequestHeader()::Failed to add request header. Header Type: " + headerType.ToString() +
                    " " + ex.ToString());
            }
        }
    }
}