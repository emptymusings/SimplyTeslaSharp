using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaMaster
{
    /// <summary>
    /// Class exposing authentication/authorization information to the Crestron processor
    /// </summary>
    public class Authentication
    {
        /// <summary>
        /// Gets or Sets the user's login email
        /// </summary>
        public string UserName
        {
            get { return Settings.UserName; }
            set
            {
                if (value != Settings.UserName)
                {
                    Settings.UserName = value;
                }
            }
        }

        /// <summary>
        /// Gets or Sets the user's login password
        /// </summary>
        public string Password
        {
            get { return Settings.Password; }
            set
            {
                if (Settings.Password != value)
                {
                    Settings.Password = value;
                }
            }
        }

        /// <summary>
        /// Gets a string representation of the token's creation date/time
        /// </summary>
        public string TokenCreated
        {
            get { return Settings.Token.CreatedTimeString; }
        }
        /// <summary>
        /// Gets a string representation of the token's expiration date/time
        /// </summary>
        public string TokenExpires
        {
            get { return Settings.Token.ExpiresString; }
        }
        /// <summary>
        /// Gets the secondary token used to refresh the auth token
        /// </summary>
        public string RefreshToken
        {
            get { return Settings.Token.refresh_token; }
        }

        /// <summary>
        /// Assigns login values for authentication
        /// </summary>
        /// <param name="username">The user's login email</param>
        /// <param name="password">The user's login password</param>
        public void Initialize(string username, string password)
        {
            Settings.UserName = username;
            Settings.Password = password;
        }

        /// <summary>
        /// Sends a request for an authorization token to Tesla's servers
        /// </summary>
        public void SendTokenRequest()
        {
            try
            {
                Tokens.Token token = Tokens.TokenGenerator.GetToken();
                Settings.Token = token;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Authentication.SendTokenRequest()::Tesla API Error (SendTokenRequest): " + ex.Message);
                ErrorLog.Exception("Tesla API Error Sending Token Request", ex);
            }
        }
        /// <summary>
        /// Exposes the current authorization token used for all requests
        /// </summary>
        public string GetCurrentToken()
        {
            try
            {
                return Settings.Token.access_token;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Authentication.SendTokenRequest()::Tesla API Error (GetCurrentToken): " + ex.Message);
                ErrorLog.Exception("Tesla API Error Accessing Token", ex);
                return null;
            }
        }
    }
}