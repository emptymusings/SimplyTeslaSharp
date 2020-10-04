using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaMaster.Data;
using SimplTeslaMaster.Results.GetResults;

namespace SimplTeslaMaster.Models
{
    /// <summary>
    /// Clsss to expose Vehicle List data to th Crestron processor
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
                    CrestronConsole.PrintLine("SimplTeslaMaster.Models.VehicleListModel.Vehicles_get::Unable to get Vehicle List: " + ex.ToString());
                    return null;
                }
            }
        }

        private void GetVehicleArray()
        {
            try
            {
                vehicles = new VehicleSummaryModel[VehicleCount];

                for (int i = 0; i < VehicleCount; i++)
                {
                    vehicles[i] = new VehicleSummaryModel(Data.response[i]);
                }
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Models.VehicleListModel.GetVehicleArray()::Error creating vehicle list. Is the data null? " +
                    (Data == null ? "Yes" : "No") + " " + ex.ToString());
            }
        }

        public void RefreshData()
        {
            VehicleListGetResult list = new VehicleListGetResult();

            try
            {
                list.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Models.VehicleListModel.RefreshData()::list.GetResults() failed " + ex.ToString());
            }

            Data = list.Result;

            try
            {
                GetVehicleArray();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaMaster.Models.VehicleListModel.RefreshData()::Error getting vehicle array: " + ex.ToString());
            }
        }

        public string GetIdFromName(string vehicleName)
        {
            string id = (from c in Vehicles where c.Name.ToLower() == vehicleName.ToLower() select c.Id).FirstOrDefault();

            return id;
        }
    }
}