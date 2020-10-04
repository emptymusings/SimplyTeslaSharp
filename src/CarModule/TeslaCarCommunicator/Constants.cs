using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar
{
    internal class Constants
    {
        internal const string URL_BASE = "https://owner-api.teslamotors.com";
        internal const string URL_AUTHORIZATION = URL_BASE + "/oauth/token";
        internal const string URL_BASE_API = URL_BASE + "/api/1";
        internal const string URL_VEHICLES = URL_BASE_API + "/vehicles";
        internal const string URL_COMMAND = "/command";
        internal const string URL_COMMAND_CHARGE_SET_LIMIT = URL_COMMAND + "/set_charge_limit";
        internal const string URL_COMMAND_CHARGE_START = URL_COMMAND + "/charge_start";
        internal const string URL_COMMAND_CHARGE_STOP = URL_COMMAND + "/charge_stop";
        internal const string URL_COMMAND_DOOR_LOCK = URL_COMMAND + "/door_lock";
        internal const string URL_COMMAND_DOOR_UNLOCK = URL_COMMAND + "/door_unlock";
        internal const string URL_COMMAND_HVAC_BASE = URL_COMMAND + "/auto_conditioning_";
        internal const string URL_COMMAND_HVAC_START = URL_COMMAND_HVAC_BASE + "start";
        internal const string URL_COMMAND_HVAC_STOP = URL_COMMAND_HVAC_BASE + "stop";
        internal const string URL_COMMAND_HVAC_SET_TEMP = URL_COMMAND + "/set_temps";
    }
}