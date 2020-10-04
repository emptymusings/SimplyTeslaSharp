using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster.Tokens
{
    /// <summary>
    /// Class representation of a Tesla authorization token
    /// </summary>
    internal class Token
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public double expires_in { get; set; }
        public string refresh_token { get; set; }
        public double created_at { get; set; }

        /// <summary>
        /// Gets the converted UNIX UTC created_at value to a DateTime data type
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return Converters.GetDateTimeFromUtc(created_at);
            }
        }

        /// <summary>
        /// Gets the converted expires_in + created_at result to a DateTime data type
        /// </summary>
        public DateTime Expires
        {
            get
            {
                return Converters.GetDateTimeFromUtc(created_at + expires_in);
            }
        }

        /// <summary>
        /// Gets a value determining if the auth token has expired
        /// </summary>
        public bool IsExpired
        {
            get
            {
                DateTime modifiedExpiration = new DateTime(Expires.Year, Expires.Month, Expires.Day - 1);

                if (DateTime.Now >= modifiedExpiration)
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// Gets the creation date/time of the auth token in a nicely formatted string
        /// </summary>
        public string CreatedTimeString
        {
            get
            {
                return CreatedTime.ToString("MM/dd/yyyy hh:mm:ss tt") + " GMT";
            }
        }
        /// <summary>
        /// Gets the expiration date/time of the auth token in a nicely formatted string
        /// </summary>
        public string ExpiresString
        {
            get
            {
                return Expires.ToString("MM/dd/yyyy hh:mm:ss tt") + " GMT";
            }
        }
    }
}