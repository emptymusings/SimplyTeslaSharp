using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using Crestron.SimplSharp.Net.Https;
using SimplTeslaMaster.WebRequests;

namespace SimplTeslaMaster.Results.PostResults
{
    /// <summary>
    /// Base class for all POST requests. Extends the BaseResult class
    /// </summary>
    /// <typeparam name="T">The Type of object expected to be returned</typeparam>
    internal abstract class PostResultBase<T> : ResultBase<T>
    {
        public PostResultBase(string apiUrl) :
            base(apiUrl)
        {
            CallType = CallTypes.Command;
        }
    }
}