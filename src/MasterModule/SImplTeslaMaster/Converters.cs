using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster
{
    internal class Converters
    {
        public static DateTime GetDateTimeFromUtc(double unixValue)
        {
            DateTime unixStart = GetNewUnixDateTime();
            DateTime dateTime = unixStart.AddSeconds(unixValue);

            return dateTime;
        }

        public static DateTime GetNewUnixDateTime()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}