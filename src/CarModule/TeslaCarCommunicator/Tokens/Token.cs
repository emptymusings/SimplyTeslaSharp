using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Tokens
{
    /// <summary>
    /// Tesla server authentication token
    /// </summary>
    public class Token
    {
        // This event is unused at this point, but could be used to trigger an action when a token is changed
        public delegate void TokenChangedEventHandler(object sender, EventArgs e);
        public event TokenChangedEventHandler TokenChanged;

        /// <summary>
        /// Token constructor.
        /// Immediately sets the expires_in value to the current default value of 3888000 (this might change in the future, but hasn't yet)
        /// </summary>
        public Token()
        {
            expires_in = 3888000;
        }

        private string accessToken;
        /// <summary>
        /// Gets or Sets the authentication token used in the header of all HTTP(S) requests
        /// </summary>
        public string access_token
        {
            get { return accessToken; }
            set
            {
                if (access_token != value)
                {
                    accessToken = value;

                    // Notify any event listeners that the token has changed
                    OnTokenChanged(this, new EventArgs());
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the token type, this is currently always 'password'
        /// </summary>
        public string token_type { get; set; }
        /// <summary>
        /// Gets or sets the amount of time from generation that the token will continue to be accepted.
        /// The current constant response is 3888000, ad is currently immediately set to that value in the constructor.
        /// This property can be SET if that ever changes.  This value is a UNIX time (double data type)
        /// </summary>
        public double expires_in { get; set; }
        /// <summary>
        /// Gets or Sets the value to send to Tesla's servers if a token has expired that allows a refresh of the token without resendig the login information form
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// UNIX UTC time at which the token was created
        /// </summary>
        public double created_at { get; set; }

        /// <summary>
        /// Triggers any assigned methods to the TokenChanged event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnTokenChanged(object sender, EventArgs e)
        {
            if (TokenChanged != null)
                TokenChanged(sender, e);
        }

        /// <summary>
        /// GETs he UNIX formatted created time as a DateTime object
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return Calculators.GetDateTimeFromUtc(created_at);
            }
        }

        /// <summary>
        /// GETs the UNIX formatted expiration as a DateTime object
        /// </summary>
        public DateTime Expires
        {
            get
            {
                return Calculators.GetDateTimeFromUtc(created_at + expires_in);
            }
        }

        /// <summary>
        /// Determines if the current authentication token has expired
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
        /// GETs the token's CreatedTime as a formatted string
        /// </summary>
        public string CreatedTimeString
        {
            get
            {
                return CreatedTime.ToString("MM/dd/yyyy hh:mm:ss tt") + " GMT";
            }
        }

        /// <summary>
        /// GETs the token's ExpirationTime as a formatted string
        /// </summary>
        public string ExpiresString
        {
            get
            {
                return Expires.ToString("MM/dd/yyyy hh:mm:ss tt") + " GMT";
            }
        }

        /// <summary>
        /// Assigns the Token's creation time based on information received the Crestron processor
        /// </summary>
        /// <param name="creationDateString">The formatted string value received from the processor</param>
        public void SetCreation(string creationDateString)
        {
            DateTime dateVal;
            
            dateVal = GetDateFromString(creationDateString);
            created_at = Calculators.GetUtcDateFromDateTime(dateVal);
        }

        /// <summary>
        /// Assigns the expiration time/date based o information received from the Crestron processor
        /// </summary>
        /// <param name="expirationDateString">The formatted string value received </param>
        public void SetExpiration(string expirationDateString)
        {
            DateTime dateVal;
            double totalVal;
            double expiresInVal;

            dateVal = GetDateFromString(expirationDateString);              // Get the DateTime value from the received string
            totalVal = Calculators.GetUtcDateFromDateTime(dateVal);          // Get the UNIX converted value from the date
            expiresInVal = totalVal - created_at;                           // Subtract the created date from the expires date
        }

        /// <summary>
        /// Converts a string value to a DateTime object
        /// </summary>
        /// <param name="stringValue">The value to be converted</param>
        private DateTime GetDateFromString(string stringValue)
        {
            string convertString;
            DateTime dateValue;

            convertString = stringValue.Replace(" GMT", "");
            dateValue = Convert.ToDateTime(convertString);

            return dateValue;
        }
    }
}