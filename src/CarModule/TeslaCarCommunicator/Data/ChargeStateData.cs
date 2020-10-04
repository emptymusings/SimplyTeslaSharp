using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

namespace SimplTeslaCar.Data
{
    /// <summary>
    /// POCO to store the Charge State data returned from Tesla's API
    /// </summary>
    internal class ChargeStateData 
    {
        public string charging_state { get; set; }
        public string fast_charger_type { get; set; }
        public string fast_charger_brand { get; set; }
        public int charge_limit_soc { get; set; }
        public int charge_limit_soc_std { get; set; }
        public int charge_limit_soc_min { get; set; }
        public int charge_limit_soc_max { get; set; }
        public bool charge_to_max_range { get; set; }
        public int max_range_charge_counter { get; set; }
        public bool fast_charger_present { get; set; }
        public decimal battery_range { get; set; }
        public decimal est_battery_range { get; set; }
        public decimal ideal_battery_range { get; set; }
        public int battery_level { get; set; }
        public decimal charge_energy_added { get; set; }
        public decimal charge_miles_added_rated { get; set; }
        public decimal charge_miles_added_ideal { get; set; }
        public int charger_voltage { get; set; }
        public int charger_pilot_current { get; set; }
        public decimal charger_actual_current { get; set; }
        public int charger_power { get; set; }
        public decimal time_to_full_charge { get; set; }
        public bool trip_charging { get; set; }
        public decimal charge_rate { get; set; }
        public bool charge_port_door_open { get; set; }
        public string conn_charge_cable { get; set; }
        public long? scheduled_charging_start_time { get; set; }
        public bool scheduled_charging_pending { get; set; }
        public bool? user_charge_enable_request { get; set; }
        public bool? charge_enable_request { get; set; }
        public int? charger_phases { get; set; }
        public string charge_port_latch { get; set; }
        public int charge_current_request { get; set; }
        public int charge_current_request_max { get; set; }
        public bool managed_charging_active { get; set; }
        public bool managed_charging_user_canceled { get; set; }
        public long? managed_charging_start_time { get; set; }
        public bool battery_heater_on { get; set; }
        public object not_enough_power_to_heat { get; set; }
        public int usable_battery_level { get; set; }
        public long timestamp { get; set; }
    }
}