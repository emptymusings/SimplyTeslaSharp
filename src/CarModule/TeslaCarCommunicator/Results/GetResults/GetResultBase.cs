using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.WebRequests;

namespace SimplTeslaCar.Results.GetResults
{
    /// <summary>
    /// Base class for all API GET Results. Extends BaseResult object
    /// </summary>
    /// <typeparam name="T">The Type of the expected return object</typeparam>
    internal abstract class GetResultBase<T> : ResultBase<T>
    {
        internal GetResultBase(string apiUrl) : 
            base(apiUrl)
        {
            CallType = CallTypes.Request;
        }
    }
}