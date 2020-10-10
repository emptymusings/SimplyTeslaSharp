using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar
{
    /// <summary>
    /// Class to handle calculations and conversions
    /// </summary>
    internal class Calculators
    {
        /// <summary>
        /// Converts a UNIX UTC value to a DateTime
        /// </summary>
        /// <param name="unixValue">The value to be converted</param>
        public static DateTime GetDateTimeFromUtc(double unixValue)
        {
            DateTime unixStart = GetNewUnixDateTime();
            DateTime dateTime = unixStart.AddSeconds(unixValue);

            return dateTime;
        }

        /// <summary>
        /// Creates a new DateTime value from a new UNIX UTC date (1/1/1970)
        /// </summary>
        public static DateTime GetNewUnixDateTime()
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        }

        /// <summary>
        /// Converts a DateTime value to a UNIX UTC date
        /// </summary>
        /// <param name="value">The value to be converted</param>
        public static double GetUtcDateFromDateTime(DateTime value)
        {
            double unixValue;
            DateTime unixStart = GetNewUnixDateTime();
            
            unixValue = (DateTime.UtcNow.Subtract(value)).TotalSeconds;

            return unixValue;
        }
        
        /// <summary>
        /// Converts a temperature value from Farenheit to Celsius
        /// </summary>
        /// <param name="temp">The value to be converted</param>
        public static decimal GetTempFarenheitFromCelsius(decimal temp)
        {
            decimal calcTemp = temp;
            decimal farenheitConversion = 1.8M;
            
            calcTemp = calcTemp * farenheitConversion; 
            calcTemp = calcTemp + 32;

            return calcTemp;
        }

        public static decimal GetTempCelsiusFromFarenheit(decimal temp)
        {
            decimal calcTemp = temp;
            decimal celsiusConversion = 1.8M;

            calcTemp = calcTemp - 32;
            calcTemp = calcTemp / celsiusConversion;

            return calcTemp;
        }

        /// <summary>
        /// Converts a temperature value from the vehicle to the user preferred unit (celsius or farenheit, as defined in the car's configuration by the user)
        /// </summary>
        /// <param name="temp">The value to be converted</param>
        /// <param name="tempUnits">The user preferred unit type</param>
        public static string GetTemperatureValue(decimal temp, string tempUnits)
        {
            decimal calcTemp;

            if (tempUnits == "F")
            {
                calcTemp = Calculators.GetTempFarenheitFromCelsius(temp);
            }
            else
            {
                calcTemp = temp;
            }

            return calcTemp.ToString();
        }
    }
}