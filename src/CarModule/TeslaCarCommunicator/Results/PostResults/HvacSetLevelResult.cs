using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;

namespace SimplTeslaCar.Results.PostResults
{
    /// <summary>
    /// Sets a vehicle's climate control temperature level. Extends BasePostResult
    /// </summary>
    internal class HvacSetLevelResult : PostResultBase<ApiResponse<CommandResultModel>>
    {
        public enum TempLocations
        {
            Driver,
            Passenger
        }

        public HvacSetLevelResult(decimal desiredLevel, TempLocations setLocation) :
            base(CommandUrls.HvacSetTemp)
        {
            string location;

            if (setLocation == TempLocations.Driver)
            {
                location = "driver_temp";
            }
            else
            {
                location = "passenger_temp";
            }

            FormValues.AddFormValue(location, desiredLevel.ToString());
        }
    }
}