using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using SimplTeslaCar.WebRequests;

namespace SimplTeslaCar.Results
{
    /// <summary>
    /// Base class for all API POST requests. Extends BasePostResult
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal abstract class PostResultBase<T> : ResultBase<T>
    {
        public PostResultBase(string apiUrl) :
            base(apiUrl)
        {
            CallType = SimplTeslaCar.WebRequests.CallTypes.Command;
            FormValues = new FormValues();
        }

        /// <summary>
        /// Gets or Sets the FormValues for the POST object
        /// </summary>
        public FormValues FormValues { get; set; }

        /// <summary>
        /// Sends the request to the API
        /// </summary>
        public override void GetResult()
        {
            Request = GetClientRequest();
            base.GetResult();
        }

        /// <summary>
        /// Creates the API request object
        /// </summary>
        /// <returns></returns>
        protected override HttpsClientRequest GetClientRequest()
        {
            Request = base.GetClientRequest();
            string formBody;

            // If there are form values, set them as the request's contet
            if (FormValues.ValueCollection.Count > 0)
            {
                HeaderFactory.AddFormHeaders(Request);
                formBody = FormValues.GetValuesAsContentDisposition();
                Request.ContentString = formBody;
                Request.FinalizeHeader();
            }
            
            return Request;
        }
    }
}