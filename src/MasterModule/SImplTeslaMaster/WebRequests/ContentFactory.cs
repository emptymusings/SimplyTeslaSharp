using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Tokens;
using Crestron.SimplSharp.Net.Https;

namespace SimplTeslaMaster.WebRequests
{
    /// <summary>
    /// Class that generates an HTTP request's content
    /// </summary>
    internal class ContentFactory
    {
        /// <summary>
        /// Creates a string value that will be the content of an HTTP request
        /// </summary>
        /// <param name="request">The request to which the conent will be assigned</param>
        /// <param name="requestType">The type of request being sent</param>
        public static void BuildRequestContentString(HttpsClientRequest request, RequestTypes requestType)
        {
            switch (requestType)
            {
                case RequestTypes.NewToken:
                    request.ContentString = GetNewTokenFormContent();
                    break;
                case RequestTypes.RefreshToken:
                    request.ContentString = GetRefreshTokenFormContent();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Creates the form content to request a new token
        /// </summary>
        private static string GetNewTokenFormContent()
        {
            StringBuilder contentBuilder = new StringBuilder();
            TokenRequestParametersV1 parameters = new TokenRequestParametersV1();

            try
            {
                contentBuilder.AppendLine();

                contentBuilder.Append(SetFormContentDisposition("grant_type", parameters.grant_type));
                contentBuilder.Append(SetFormContentDisposition("client_id", parameters.client_id));
                contentBuilder.Append(SetFormContentDisposition("client_secret", parameters.client_secret));
                contentBuilder.Append(SetFormContentDisposition("email", parameters.email));
                contentBuilder.Append(SetFormContentDisposition("password", parameters.password));

                contentBuilder.Append(FinalizeFormDisposition());
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequests.ContentFactory.GetNewTokenFormContent()::Failed to get form content " + ex.ToString());
            }

            return contentBuilder.ToString();
        }

        /// <summary>
        /// Creates the form content to request a token refresh
        /// </summary>
        /// <returns></returns>
        private static string GetRefreshTokenFormContent()
        {
            StringBuilder contentBuilder = new StringBuilder();
            TokenRequestParametersV1 parameters = new TokenRequestParametersV1();

            contentBuilder.AppendLine();

            try
            {
                contentBuilder.Append(SetFormContentDisposition("grant_type", "refresh_token"));
                contentBuilder.Append(SetFormContentDisposition("client_id", parameters.client_id));
                contentBuilder.Append(SetFormContentDisposition("client_secret", parameters.client_secret));
                contentBuilder.Append(SetFormContentDisposition("refresh_token", Settings.Token.refresh_token));

                contentBuilder.Append(FinalizeFormDisposition());
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.WebRequests.ContentFactory.GetRefreshTokenFormContent()::Failed to get form content " + ex.ToString());
            }

            return contentBuilder.ToString();
        }

        /// <summary>
        /// Creates the content disposition (field/value) assignment for a form field
        /// </summary>
        /// <param name="fieldName">The name of the form's field</param>
        /// <param name="value">The value assigned to the form's field</param>
        public static string SetFormContentDisposition(string fieldName, string value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--" + HeaderFactory.FORM_BOUNDARY);
            sb.AppendLine("Content-Disposition: form-data; name=\"" + fieldName + "\"");
            sb.AppendLine();
            sb.AppendLine(value);

            return sb.ToString();
        }

        /// <summary>
        /// Let's the server know that the form has been completed by finalizing it in the HTTP content
        /// </summary>
        public static string FinalizeFormDisposition()
        {
            return "--" + HeaderFactory.FORM_BOUNDARY + "--";
        }
    }
}