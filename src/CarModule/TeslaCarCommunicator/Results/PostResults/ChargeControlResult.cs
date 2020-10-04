using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;

namespace SimplTeslaCar.Results.PostResults
{
    /// <summary>
    /// Requests battery charging start/stop.  Extends BasePostResult
    /// </summary>
    internal class ChargeControlResult : PostResultBase<ApiResponse<CommandResultModel>>
    {
        public enum ChargeStates
        {
            Start,
            Stop
        }

        public ChargeControlResult(ChargeStates commandType) :
            base(
                (commandType == ChargeStates.Start ? CommandUrls.ChargeStart : CommandUrls.ChargeStop)
            )
        {

        }
    }
}