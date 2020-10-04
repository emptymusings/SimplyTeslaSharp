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
    /// Exposes Charge State data to the Crestron processor
    /// </summary>
    public class ChargingStateModel
    {
        public ChargingStateModel()
        {
            Data = new ChargeStateData();
        }

        internal ChargingStateModel(ChargeStateData data)
        {
            Data = data;
        }

        internal ChargeStateData Data { get; set; }

        public string ChargingStatus { get { return Data.charging_state; } }
        public string FastChargerType { get { return Data.fast_charger_type; } }
        public string FastChargerBrand { get { return Data.fast_charger_brand; } }
        public int ChargeLimitSoc { get { return Data.charge_limit_soc; } }
        public int ChargeLimitSocStd { get { return Data.charge_limit_soc_std; } }
        public int ChargeLimitSocMin { get { return Data.charge_limit_soc_min; } }
        public int ChargeLimitSocMax { get { return Data.charge_limit_soc_max; } }
        public int ChargeToMaxRange { get { return (Data.charge_to_max_range ? 1 : 0); } }
        public int MaxRageChargeCounter { get { return Data.max_range_charge_counter; } }
        public int FastChargerPresent { get { return Data.fast_charger_present ? 1 : 0; } }
        public string BatteryRange { get { return Data.battery_range.ToString(); } }
        public string EstimatedBatteryRange { get { return Data.est_battery_range.ToString(); } }
        public string IdealBatteryRange { get { return Data.ideal_battery_range.ToString(); } }
        public int BatteryLevel { get { return Data.battery_level; } }
        public string ChargeEnergyAdded { get { return Data.charge_energy_added.ToString(); } }
        public string ChargeMilesAddedRated { get { return Data.charge_miles_added_rated.ToString(); } }
        public string ChargeMilesAddedIdeal { get { return Data.charge_miles_added_ideal.ToString(); } }
        public string ChargerVoltage { get { return Data.charger_voltage.ToString(); } }
        public string ChargerPilotCurrent { get { return Data.charger_pilot_current.ToString(); } }
        public string ChargerActualCurrent { get { return Data.charger_actual_current.ToString(); } }
        public string ChargerPower { get { return Data.charger_power.ToString(); } }
        public string TimeToFullCharge { get { return Data.time_to_full_charge.ToString(); } }
        public int TripCharging { get { return Data.trip_charging ? 1 : 0; } }
        public string ChargeRate { get { return Data.charge_rate.ToString(); } }
        public int ChargePortDoorOpen { get { return Data.charge_port_door_open ? 1 : 0; } }
        public string ConnectedChargingCable { get { return Data.conn_charge_cable; } }
        public string ScheduledChargingTime { get { return Data.scheduled_charging_start_time.ToString(); } }
        public int ScheduledChargingPending { get { return Data.scheduled_charging_pending ? 1 : 0; } }
        public object UserChargeEnableRequest 
        { 
            get 
            {
                return Data.user_charge_enable_request.HasValue ? (Data.user_charge_enable_request.Value ? 1 : 0) : 0; 
            } 
        }
        public int ChargeEnableRequest 
        { 
            get 
            {
                int result;

                if (Data.charge_enable_request.HasValue == false)
                    result = 0;
                else if (Data.charge_enable_request.Value == true)
                    result = 1;
                else
                    result = 0;

                return result;
            } 
        }
        public object ChargerPhases { get { return Data.charger_phases; } }
        public string ChargePortLatch { get { return Data.charge_port_latch; } }
        public string ChargeCurrentRequest { get { return Data.charge_current_request.ToString(); } }
        public string ChargeCurrentRequestMax { get { return Data.charge_current_request_max.ToString(); } }
        public int ManagedChargingActive { get { return Data.managed_charging_active ? 1 : 0; } }
        public int ManagedChargingUserCanceled { get { return Data.managed_charging_user_canceled ? 1 : 0; } }
        public string ManagedChargingStartTime { get { return Data.managed_charging_start_time.ToString(); } }
        public int BatteryHeaterOn { get { return Data.battery_heater_on ? 1 : 0; } }
        public object NotEnoughPowerToHeat { get { return Data.not_enough_power_to_heat; } }
        public long Timestamp { get { return Data.timestamp; } }

        public void RefreshData(string vehicleIdString)
        {
            ChargingGetResult result = new ChargingGetResult(vehicleIdString);
            result.GetResult();
            Data = result.Result.response;
        }
    }
}