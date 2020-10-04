using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;
using SimplTeslaCar.Tokens;

namespace SimplTeslaCar.WebRequests
{
    /// <summary>
    /// Factory to create the HTTP request's content.  Currently, only returns RefreshToken actions, but could be utilized for more
    /// in the future if Tesla expads functionality
    /// </summary>
    public class ContentFactory
    {
        /// <summary>
        /// Creates the request's body as a string
        /// </summary>
        /// <param name="request">REQUIRED: the HttpsClientRequest object to assign the content to</param>
        /// <param name="requestType">The type of request from the header object</param>
        internal static void BuildRequestContentString(Crestron.SimplSharp.Net.Https.HttpsClientRequest request, RequestTypes requestType)
        {
            switch (requestType)
            {
                case RequestTypes.RefreshToken:                   // Creates the form content to request a Token refresh
                    request.ContentString = GetRefreshTokenFormContent();
                    break;
                default:
                    break;
            } 
        }

        /// <summary>
        /// Creates the form content to request a refresh of an authentication token
        /// </summary>
        private static string GetRefreshTokenFormContent()
        {
            StringBuilder contentBuilder = new StringBuilder();
            TokenRequestParameters parameters = new TokenRequestParameters();

            contentBuilder.AppendLine();

            contentBuilder.Append(SetFormContentDisposition("grant_type", "refresh_token"));
            contentBuilder.Append(SetFormContentDisposition("client_id", parameters.client_id));
            contentBuilder.Append(SetFormContentDisposition("client_secret", parameters.client_secret));
            contentBuilder.Append(SetFormContentDisposition("refresh_token", Settings.Token.refresh_token));

            contentBuilder.Append(FinalizeFormDisposition());

            return contentBuilder.ToString();
        }

        /// <summary>
        /// Adds a form value to the content disposition
        /// </summary>
        /// <param name="fieldName">The name of the form field</param>
        /// <param name="value">The value to be assigned to the form field</param>
        public static string SetFormContentDisposition(string fieldName, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--" + HeaderFactory.FormBoundary);
            sb.AppendLine("Content-Disposition: form-data; name=\"" + fieldName + "\"");
            sb.AppendLine();
            sb.AppendLine(value);

            return sb.ToString();
        }

        /// <summary>
        /// Finalizes the form value set
        /// </summary>
        public static string FinalizeFormDisposition()
        {
            return "--" + HeaderFactory.FormBoundary + "--";
        }
    }
}