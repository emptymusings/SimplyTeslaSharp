using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;

namespace SimplTeslaCar.Results.PostResults
{
    internal class HvacSetSteeringWheelHeaterResult : PostResultBase<ApiResponse<CommandResultModel>>
    {
        public HvacSetSteeringWheelHeaterResult(int turnOn)
            : base(CommandUrls.HvacSetSteeringWheelHeater)
        {
            FormValues.AddFormValue("on", (turnOn == 0 ? "false" : "true"));
        }
    }
}