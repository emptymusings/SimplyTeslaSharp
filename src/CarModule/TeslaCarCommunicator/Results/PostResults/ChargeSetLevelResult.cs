using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;


namespace SimplTeslaCar.Results.PostResults
{
    /// <summary>
    /// Requests the battery charger to be set to the specified percent. Extends BasePostResult
    /// </summary>
    internal class ChargeSetLevelResult : PostResultBase<ApiResponse<CommandResultModel>>
    {
        internal ChargeSetLevelResult(int desiredPercent) :
            base(CommandUrls.ChargeSetBatteryLevel)
        {
            FormValues.AddFormValue("percent", desiredPercent.ToString());
        }        
    }
}