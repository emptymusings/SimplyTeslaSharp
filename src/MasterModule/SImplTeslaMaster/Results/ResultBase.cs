using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using Newtonsoft.Json;
using SimplTeslaMaster.WebRequests;

namespace SimplTeslaMaster.Results
{
    /// <summary>
    /// Base clsss for all API request results
    /// </summary>
    /// <typeparam name="T">The type of object expected to be returned from Tesla's servers</typeparam>
    internal class ResultBase<T>
    {
        public ResultBase(string apiUrl)
        {
            ApiUrl = apiUrl;
        }

        internal CallTypes CallType { get; set; }

        public virtual string ApiUrl { get; set; }

        public virtual T Result { get; set; }

        protected virtual HttpsClient Client { get; set; }
        protected virtual HttpsClientRequest Request { get; set; }
        protected virtual HttpsClientResponse Response { get; set; }

        public virtual void GetResult()
        {
            T result = default(T);
            string jsonResponse;
            Type type = typeof(T);

            try
            {
                jsonResponse = GetApiResponse();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult.GetResult()::Failed to get API response " + ex.ToString());
                jsonResponse = null;
            }

            try
            {
                result = (T)JsonConvert.DeserializeObject(jsonResponse, type);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.SimplTeslaMaster.Results.BaseResult.GetResult()::Unable to parse call results." + ex.ToString());
            }

            Result = result;
        }

        protected virtual string GetApiResponse()
        {
            Client = HttpsClientFactory.NewClient();
            int responseCode;
            string responseString;

            try
            {
                if (Request == null)
                    Request = GetClientRequest();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult.GetApiResponse()::Failed to get client request " + ex.ToString());
            }

            Request.Url.Parse(ApiUrl);

            Client.Url = Request.Url;

            try
            {
                Request.FinalizeHeader();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult.GetApiResponse()::Failed to finalize header " + ex.ToString());
            }

            
            Response = Client.Dispatch(Request);
            
            responseCode = Response.Code;
            responseString = Response.ContentString;

            if (responseCode != 200)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult.GetApiResponse()::Request was not successful!");
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult>GetApiResponse()::Response code: " + responseCode.ToString());
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult>GetApiResponse()::Response content: " + responseString);
            }

            Request = null;
            Client.Dispose();
            Client = null;

            return Response.ContentString;
        }

        protected virtual HttpsClientRequest GetClientRequest()
        {
            HttpsClientRequest result = null;

            try
            {
                result = HttpsRequestFactory.GetClientRequest(CallType);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Results.BaseResult.GetClientRequest()::Failed to create client request for call type '" + CallType.ToString() + "' " + ex.ToString());
            }

            return result;
        }
    }
}