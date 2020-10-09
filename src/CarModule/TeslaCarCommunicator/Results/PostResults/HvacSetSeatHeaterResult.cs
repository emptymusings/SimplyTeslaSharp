using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Models;

namespace SimplTeslaCar.Results.PostResults
{
    internal class HvacSetSeatHeaterResult : PostResultBase<ApiResponse<CommandResultModel>>
    {
        public enum SeatHeaterPositions
        {
            SeatHeaterFrontLeft = 0,
            SeatHeaterFrontRight = 1,
            SeatHeaterRearLeft = 2,
            SeatHeaterRearLeftBack = 3,
            SeatHeaterRearCenter = 4,
            SeatHeaterRearRight = 5,
            SeatHeaterRearRightBack = 6,
            SeatHeater3rdRowLeft = 7,
            SeatHeater3rdRowRight = 8
        }
        
        public HvacSetSeatHeaterResult(SeatHeaterPositions position, int level) :
            base(CommandUrls.HvacSetSeatHeater)
        {
            FormValues.AddFormValue("heater", ((int)position).ToString());
            FormValues.AddFormValue("level", level.ToString());
        }
    }
}