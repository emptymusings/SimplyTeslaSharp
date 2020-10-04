using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;
using SimplTeslaCar.Results;
using SimplTeslaCar.Results.GetResults;

namespace SimplTeslaCar.Models
{
    /// <summary>
    /// Exposes Vehicle List data to the Crestron processor
    /// </summary>
    public class VehicleListModel
    {
        public VehicleListModel() 
        {
            Data = new VehicleListData();
        }

        internal VehicleListModel(VehicleListData vehicleListData)
        {
            Data = vehicleListData;
        }

        internal VehicleListData Data { get; set; }

        public int VehicleCount
        {
            get { return Data.count; }
        }

        private VehicleSummaryModel[] vehicles;

        public VehicleSummaryModel[] Vehicles
        {
            get
            {
                try
                {
                    GetVehicleArray();
                    return vehicles;
                }
                catch (Exception ex)
                {
                    CrestronConsole.PrintLine("SimplTeslaCar.Models.VehicleListModel.Vehicles_get::Unable to get Vehicle List: " + ex.ToString());
                    return null;
                }
            }
        }

        private void GetVehicleArray()
        {
            vehicles = new VehicleSummaryModel[VehicleCount];

            for (int i = 0; i < VehicleCount; i++)
            {
                vehicles[i] = new VehicleSummaryModel(Data.response[i]);
            }
        }

        public void RefreshData()
        {
            VehicleListGetResult list = new VehicleListGetResult();

            list.GetResult();
            Data = list.Result;
            GetVehicleArray();
        }

        public string GetIdFromName(string vehicleName)
        {
            string id = (from c in Vehicles where c.Name.ToLower() == vehicleName.ToLower() select c.Id).FirstOrDefault();

            return id;
        }
    }
}