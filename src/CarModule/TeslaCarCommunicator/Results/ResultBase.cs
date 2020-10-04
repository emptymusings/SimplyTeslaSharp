using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using Newtonsoft.Json;
using SimplTeslaCar.WebRequests;

namespace SimplTeslaCar.Results
{
    /// <summary>
    /// Base object from which all API results extend
    /// </summary>
    /// <typeparam name="T">The object type expected to be contained in the server's response</typeparam>
    internal class ResultBase<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="apiUrl">The URL to which the request will be sent</param>
        public ResultBase(string apiUrl)
        {
            ApiUrl = apiUrl;
        }

        /// <summary>
        /// Gets or Sets type of API call to be made (GET/POST)
        /// </summary>
        internal CallTypes CallType { get; set; }

        /// <summary>
        /// Gets or Sets the URL to which the response will be sent
        /// </summary>
        public virtual string ApiUrl { get; set; }

        /// <summary>
        /// Gets or Sets the results from the API
        /// </summary>
        public virtual T Result { get; set; }

        /// <summary>
        /// Gets or Sets the web client
        /// </summary>
        protected virtual HttpsClient Client { get; set; }
        /// <summary>
        /// Gets or Sets the web request
        /// </summary>
        protected virtual HttpsClientRequest Request { get; set; }
        /// <summary>
        /// Gets or Sets the web response
        /// </summary>
        protected virtual HttpsClientResponse Response { get; set; }

        /// <summary>
        /// Retrieves the results from a web request
        /// </summary>
        public virtual void GetResult()
        {
            T result = default(T);
            string jsonResponse;

            // Get the JSON value from the web request
            jsonResponse = GetApiResponse();

            try
            {
                // Deserialize the JSON into an object
                result = (T)JsonConvert.DeserializeObject(jsonResponse, typeof(T));                
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Results.BaseResult()::Unable to parse call results." + ex.ToString());
            }

            Result = result;
        }

        /// <summary>
        /// Sends the web request to the API
        /// </summary>
        /// <returns>The API's response</returns>
        protected virtual string GetApiResponse()
        {
            int responseCode;
            string responseString;

            Client = HttpsClientFactory.NewClient();

            if (Request == null)
                Request = GetClientRequest();

            Request.Url.Parse(ApiUrl);
            Client.Url = Request.Url;

            Request.FinalizeHeader();
            Response = Client.Dispatch(Request);

            // Retrieving the response code serves 2 purposes: 
            //      1) It ensures that the thread has waited for the Dispatch request to have completed before proceeding, preventing null results; and
            //      2) It allows verification of success (200)
            responseCode = Response.Code;
            responseString = Response.ContentString;

            if (responseCode != 200)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Results.BaseResult.GetApiResponse()::Request was not successful!");
                CrestronConsole.PrintLine("SimplTeslaCar.Results.BaseResult>GetApiResponse()::Response code: " + responseCode.ToString());
                CrestronConsole.PrintLine("SimplTeslaCar.Results.BaseResult>GetApiResponse()::Response content: " + responseString);
            }

            Request = null;
            Client.Dispose();
            Client = null;

            return Response.ContentString;
        }

        /// <summary>
        /// Creates a new HTTPS client request
        /// </summary>
        protected virtual HttpsClientRequest GetClientRequest()
        {
            HttpsClientRequest result;

            result = HttpsRequestFactory.GetClientRequest(CallType);

            return result;
        }
    }
}