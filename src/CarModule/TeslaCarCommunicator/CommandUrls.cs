using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar
{
    /// <summary>
    /// Class to compile API request commands
    /// </summary>
    public class CommandUrls
    {
        private static string baseCommandUrl = Constants.URL_VEHICLES + "/" + Settings.CarId;      // The base of all requests to communicate with a care

        /// <summary>
        /// Gets the URL used to set the level to which the car's battery will charge
        /// </summary>
        public static string ChargeSetBatteryLevel
        {
            get
            {
                string result;

                result = baseCommandUrl + Constants.URL_COMMAND_CHARGE_SET_LIMIT;

                return result;
            }
        }

        /// <summary>
        /// Gets the URL used to send a start command for battery charging
        /// </summary>
        public static string ChargeStart
        {
            get
            {
                string result;

                result = baseCommandUrl + Constants.URL_COMMAND_CHARGE_START;

                return result;
            }
        }

        /// <summary>
        /// Gets the URL used to send a stop command for battery charging
        /// </summary>
        public static string ChargeStop
        {
            get
            {
                string result;

                result = baseCommandUrl + Constants.URL_COMMAND_CHARGE_STOP;

                return result;
            }
        }

        /// <summary>
        /// Gets the URL used to send a lock command for the car's doors
        /// </summary>
        public static string DoorLock
        {
            get
            {
                string results;

                results = baseCommandUrl + Constants.URL_COMMAND_DOOR_LOCK;

                return results;
            }
        }

        /// <summary>
        /// Gets the URL used to send an unlock command for the car's doors
        /// </summary>
        public static string DoorUnlock
        {
            get
            {
                string results;

                results = baseCommandUrl + Constants.URL_COMMAND_DOOR_UNLOCK;

                return results;
            }
        }

        /// <summary>
        /// Gets the URL used to set the temperature of the car's climate control
        /// </summary>
        public static string HvacSetTemp
        {
            get
            {
                string result;

                result = baseCommandUrl + Constants.URL_COMMAND_HVAC_SET_TEMP;

                return result;
            }
        }

        /// <summary>
        /// Gets the URL used to start the car's climate control
        /// </summary>
        public static string HvacStart
        {
            get
            {
                string result;

                result = baseCommandUrl + Constants.URL_COMMAND_HVAC_START;

                return result;
            }
        }

        /// <summary>
        /// Gets the URL used to stop the car's climate control
        /// </summary>
        public static string HvacStop
        {
            get
            {
                string result;

                result = baseCommandUrl + Constants.URL_COMMAND_HVAC_STOP;

                return result;
            }
        }
    }
}