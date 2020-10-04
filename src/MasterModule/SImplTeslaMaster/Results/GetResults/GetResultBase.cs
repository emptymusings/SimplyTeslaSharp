using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.WebRequests;

namespace SimplTeslaMaster.Results.GetResults
{
    /// <summary>
    /// Base class for all GET requests. Extends BaseResults class
    /// </summary>
    /// <typeparam name="T">The Type of data expected to be returned</typeparam>
    internal abstract class GetResultBase<T> : ResultBase<T>
    {
        internal GetResultBase(string apiUrl) :
            base(apiUrl)
        {
            CallType = CallTypes.Request;
        }
    }
}