using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Tokens;

namespace SimplTeslaMaster
{
    internal static class Settings
    {
        private static string userName;
        /// <summary>
        /// Gets or Sets the user's login email
        /// </summary>
        internal static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private static string password;
        /// <summary>
        /// Gets or Sets the user's login password
        /// </summary>
        internal static string Password
        {
            get { return password; }
            set { password = value; }
        }


        private static TokenV1 token;
        /// <summary>
        /// Gets or Sets authoriztion token for Tesla's servers
        /// </summary>
        internal static TokenV1 Token
        {
            get
            {
                if (token == null ||
                    token.IsExpired)
                {
                    GetToken();
                }

                return token;
            }
            set { token = value; }
        }

        /// <summary>
        /// Gets a value indicating whether a token has been received from Tesla's servers
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
        /// Requests a token from Tesla's servers
        /// </summary>
        private static void GetToken()
        {
            token = TokenGeneratorV1.GetToken();
        }
    }
}