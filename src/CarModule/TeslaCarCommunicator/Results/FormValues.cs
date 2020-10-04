using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.WebRequests;

namespace SimplTeslaCar.Results
{
    /// <summary>
    /// Object containing a collection of values to be submitted as elements of a web form request
    /// </summary>
    internal class FormValues
    {
        private List<FormValue> valueCollection = new List<FormValue>();
        /// <summary>
        /// Gets or Sets the colleciton of values
        /// </summary>
        public List<FormValue> ValueCollection
        {
            get { return valueCollection; }
            set { valueCollection = value; }
        }

        /// <summary>
        /// Adds a FormValue to the ValueCollection
        /// </summary>
        /// <param name="field">The name of the Form field</param>
        /// <param name="value">The value of the Form field</param>
        public void AddFormValue(string field, string value)
        {
            ValueCollection.Add(new FormValue(field, value));
        }

        /// <summary>
        /// Gathers all values in the ValueCollection as a compiled HTTP form for submission
        /// </summary>
        public string GetValuesAsContentDisposition()
        {
            StringBuilder results = new StringBuilder();
            string dispositionValue;

            if (ValueCollection.Count == 0)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Results.FormValues.GetValueAsContentDisposition()::VALUECOLLECTION IS EMPTY!");
                return "";
            }
            
            foreach (FormValue fv in ValueCollection)
            {
                dispositionValue = ContentFactory.SetFormContentDisposition(fv.Field, fv.Value);
                results.Append(dispositionValue);
            }


            results.Append(ContentFactory.FinalizeFormDisposition());

            return results.ToString();
        }
    }
}