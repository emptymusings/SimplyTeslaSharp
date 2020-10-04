using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Tokens;

namespace SimplTeslaCar
{
    /// <summary>
    /// Class containing long-lived variables
    /// </summary>
    internal static class Settings
    {
        /// <summary>
        /// Gets or Sets the ID of the vehicle to be communicated with through this module
        /// </summary>
        internal static string CarId { get; set; }

        /// <summary>
        /// Gets or Sets the unit type that the user has selected for temperature.  Either celsius (C) or farenheit (F)
        /// </summary>
        internal static string TemperatureUnits { get; set; }

        private static Token token;
        /// <summary>
        /// Gets or Sets the authentication token for the API
        /// </summary>
        internal static Token Token
        {
            get
            {
                return token;
            }
            set 
            { 
                token = value;
            }
        }

        /// <summary>
        /// Gets or Sets a value indicating whether the Token has been set
        /// </summary>
        public static bool TokenInitialized { get; set; }

        /// <summary>
        /// Gets the value determining if the authentication token has been set
        /// </summary>
        internal static bool TokenExists
        {
            get
            {
                return token != null &&
                    !string.IsNullOrEmpty(token.access_token);
            }
        }

        /// <summary>
        /// Gets a Token from Tesla's authentication server
        /// </summary>
        private static void GetToken()
        {
            Token = TokenGenerator.GetToken();
        }

        /// <summary>
        /// Gets the Authorization key to be used in the header of all API calls
        /// </summary>
        internal static KeyValuePair<string, string> GetAuthorizationKey()
        {
            return new KeyValuePair<string, string>("Authorization", "Bearer {" + Token.access_token + "}");
        }
    }
}