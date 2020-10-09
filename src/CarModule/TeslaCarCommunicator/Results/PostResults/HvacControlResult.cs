using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;

namespace SimplTeslaCar.Results.PostResults
{
    /// <summary>
    /// Turns a vehicles climate control on or off. Extends BasePostResult
    /// </summary>
    internal class HvacControlResult : PostResultBase<ApiResponse<CommandResultModel>>
    {
        public enum HvacStates
        {
            Start,
            Stop
        }

        public HvacControlResult(HvacStates setToState) :
            base(
                (setToState == HvacStates.Start ? CommandUrls.HvacStart : CommandUrls.HvacStop)
            )
        {

        }
    }
}