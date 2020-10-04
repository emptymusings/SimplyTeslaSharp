using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Tokens
{
    /// <summary>
    /// Parameters associated with a Token Request from Tesla's authentication servers
    /// </summary>
    internal class TokenRequestParameters
    {
        private string _grant_type = "password";
        /// <summary>
        /// Gets or Sets the grant_type field to send as a form value (currently constant as 'password'
        /// </summary>
        public string grant_type
        {
            get { return _grant_type; }
            set { _grant_type = value; }
        }

        private string _client_id = "81527cff06843c8634fdc09e8ac0abefb46ac849f38fe1e431c2ef2106796384";
        /// <summary>
        /// Gets or Sets the client_id field to send as a form value (currently constant, change the _client_id assignment if it ever changes)
        /// </summary>
        public string client_id
        {
            get { return _client_id; }
            set { _client_id = value; }
        }

        private string _client_secret = "c7257eb71a564034f9419ee651c7d0e5f7aa6bfbd18bafb5c5c033b093bb2fa3";
        /// <summary>
        /// Gets or sets the client_secret field to send as a form value (currently constant)
        /// </summary>
        public string client_secret
        {
            get { return _client_secret; }
            set { _client_secret = value; }
        }
    }
}