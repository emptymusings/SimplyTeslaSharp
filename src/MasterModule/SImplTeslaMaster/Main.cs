using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Models;

namespace SimplTeslaMaster
{
    /// <summary>
    /// The primary class exposing all properties and functions to the Crestron processor
    /// </summary>
    public class Main
    {
        public Main()
        {
            // Instantiate variables
            Auth = new Authentication();
            cars = new VehicleListModel();
        }

        /// <summary>
        /// Gets or Sets authorization and authentication information
        /// </summary>
        public Authentication Auth { get; set; }
        
        private VehicleListModel cars;
        /// <summary>
        /// Gets or Sets the list of vehicles associated with the user's Tesla account
        /// </summary>
        public VehicleListModel Cars
        {
            get { return cars; }
        }
        
        /// <summary>
        /// Gets or Sets the number of cars associated with the user's Tesla account
        /// </summary>
        public short CarCount
        {
            get
            {
                if (cars == null)
                    return 0;
                else
                    return (short)cars.VehicleCount;
            }
        }

        /// <summary>
        /// Refreshes the list of cars
        /// </summary>
        public void RefreshCars()
        {
            if (cars == null)
            {
                cars = new VehicleListModel();
            }

            try
            {
                cars.RefreshData();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Main.RefreshCars()::Error Refreshing Cars: " + ex.ToString());
            }
        }
    }
}