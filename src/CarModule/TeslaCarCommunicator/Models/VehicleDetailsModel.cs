using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Data;

namespace SimplTeslaCar.Models
{
    /// <summary>
    /// Exposes Vehicle Detail data to the Crestron processor
    /// </summary>
    public class VehicleDetailsModel
    {
        public VehicleDetailsModel()
        {
            Data = new VehicleDetailData();
        }

        internal VehicleDetailsModel(VehicleDetailData data)
        {
            Data = data;
        }

        internal VehicleDetailData Data { get; set; }

        #region Properties

        #region Car Summary

        public string Name { get { return Data.display_name; } }
        public string Status { get { return Data.state; } }
        public short InService { get { return Data.in_service ? (short)1 : (short)0; } }
        public string Id { get { return Data.id_s; }}
        public short CalendarEnabled { get { return Data.calendar_enabled ? (short)1 : (short)0; } }
        public string Vin { get { return Data.vin; } }

        #endregion

        #region Charge State

        public short BatteryLevel { get { return (short)Data.charge_state.battery_level; } }
        public string BatteryRange { get { return Data.charge_state.battery_range.ToString(); } }
        public short BatteryChargeAmpsUserDefined { get { return (short)Data.charge_state.charge_current_request; } }
        public short BatteryChargeAmpsMax { get { return (short)Data.charge_state.charge_current_request_max; } }
        public short BatteryChargeEnableRequest 
        { 
            get 
            {
                return Data.charge_state.charge_enable_request.HasValue ? (Data.charge_state.charge_enable_request.Value ? (short)1 : (short)0) : (short)0; 
            }
        }
        public string BatteryLastChargeEnergyAddedKwh { get { return Data.charge_state.charge_energy_added.ToString(); } }
        public string BatteryChargeLimitValue { get { return Data.charge_state.charge_limit_soc.ToString() + "%"; } }
        public string BatteryLastChargeMilesAdded { get { return Data.charge_state.charge_miles_added_ideal.ToString(); } }
        public short BatteryChargePortDoorOpen { get { return Data.charge_state.charge_port_door_open ? (short)1 : (short)0; } }
        public string BatteryChargePortLatchStatus { get { return Data.charge_state.charge_port_latch; } }
        public string BatteryChargeRate { get { return Data.charge_state.charge_rate.ToString(); } }
        public short BatteryChargeToMaxRange { get { return Data.charge_state.charge_to_max_range ? (short)1 : (short)0; } }
        public string BatteryChargeActualCurrent { get { return Data.charge_state.charger_actual_current.ToString(); } }
        public short BatteryChargerPhases { get { return (Data.charge_state.charger_phases.HasValue ? (short)Data.charge_state.charger_phases.Value : (short)0); } }
        public short BatteryChargerPilotCurrentAmps { get { return (short)Data.charge_state.charger_pilot_current; } }
        public short BatteryChargerPowerKwh { get { return (short)Data.charge_state.charger_power; } }
        public short BatteryChargerVoltage { get { return (short)Data.charge_state.charger_voltage; } }
        public string BatteryChargingStatus { get { return Data.charge_state.charging_state; } }
        public string BatteryEstimatedRange { get { return Data.charge_state.est_battery_range.ToString(); } }
        public string BatteryIdealRange { get { return Data.charge_state.ideal_battery_range.ToString(); } }
        public short BatteryScheduledChargePending { get { return Data.charge_state.scheduled_charging_pending ? (short)1 : (short)0; } }
        public string BatteryScheduledChargeStartTime 
        { 
            get 
            { 
                if (Data.charge_state.scheduled_charging_start_time.HasValue)
                {
                    return Calculators.GetDateTimeFromUtc(Convert.ToDouble(Data.charge_state.scheduled_charging_start_time.Value)).ToString("dd/MM/yyyy hh:mm tt");
                }
                else
                {
                    return "Not Set";
                }
            } 
        }
        public string BatteryHoursToFullCharge { get { return Data.charge_state.time_to_full_charge.ToString(); } }
        public short BatteryIsChargingForTrip { get { return Data.charge_state.trip_charging ? (short)1 : (short)0; } }
        public short BatteryUsableChargeLevel { get { return (short)Data.charge_state.usable_battery_level; } }

        #endregion

        #region Climate State

        public short BatteryHeater { get { return Data.climate_state.battery_heater ? (short)1 : (short)0; } }
        public short BatteryHeaterNoPower 
        { 
            get 
            {
                return (short)(Data.climate_state.battery_heater_no_power.HasValue ? (Data.climate_state.battery_heater_no_power.Value == true ? 1 : 0) : 0); 
            } 
        }
        public string ClimateDriverTempSetting { get { return Calculators.GetTemperatureValue(Data.climate_state.driver_temp_setting, Data.gui_settings.gui_temperature_units); } }
        public short ClimateFanLevel { get { return (short)Data.climate_state.fan_status; } }
        public string ClimateInsideTemp { get { return Calculators.GetTemperatureValue(Data.climate_state.inside_temp, Data.gui_settings.gui_temperature_units); } }
        public short ClimateIsAutoConditioningOn { get { return Data.climate_state.is_auto_conditioning_on ? (short)1 : (short)0; } }
        public short ClimateIsHvacOn { get { return (short)(Data.climate_state.is_climate_on ? 1 : 0); } }
        public short ClimateIsFrontDefrosterOn { get { return (short)(Data.climate_state.is_front_defroster_on ? 1 : 0); } }
        public short ClimateIsPreconditioning { get { return (short)(Data.climate_state.is_preconditioning ? 1 : 0); } }
        public short ClimateIsRearDefrosterOn { get { return (short)(Data.climate_state.is_rear_defroster_on ? 1 : 0); } }
        public short ClimateLeftVentDirection { get { return (short)(Data.climate_state.left_temp_direction); } }
        public string ClimateAvailableTempMax { get { return Calculators.GetTemperatureValue(Data.climate_state.max_avail_temp, Data.gui_settings.gui_temperature_units); } }
        public string ClimateAvailableTempMin { get { return Calculators.GetTemperatureValue(Data.climate_state.min_avail_temp, Data.gui_settings.gui_temperature_units); } }
        public string ClimateOutsideTemp { get { return Calculators.GetTemperatureValue(Data.climate_state.outside_temp, Data.gui_settings.gui_temperature_units); } }
        public string ClimatePassengerTempSetting { get { return Calculators.GetTemperatureValue(Data.climate_state.passenger_temp_setting, Data.gui_settings.gui_temperature_units); } }
        public short ClimateRghtVentDirection { get { return (short)(Data.climate_state.right_temp_direction); } }
        public short ClimateSeatHeaterDriver { get { return (short)Data.climate_state.seat_heater_left; } }
        public short ClimateSeatHeaterRearCenter { get { return (short)(Data.climate_state.seat_heater_rear_center); } }
        public short ClimateSeatHeaterRearLeft { get { return (short)(Data.climate_state.seat_heater_rear_left); } }
        public short ClimateSeatHeaterRearLeftBack { get { return (short)(Data.climate_state.seat_heater_rear_left_back); } }
        public short ClimateSeatHeaterRearRight { get { return (short)(Data.climate_state.seat_heater_rear_right); } }
        public short ClimateSeatHeaterRearRightBack { get { return (short)(Data.climate_state.seat_heater_rear_right_back); } }
        public short ClimateSeatHeaterPassenger { get { return (short)(Data.climate_state.seat_heater_right); } }
        public short ClimateSideMirrorHeaters { get { return (short)(Data.climate_state.side_mirror_heaters ? 1 : 0); } }
        public short ClimateSmartPreconditioning { get { return (short)(Data.climate_state.smart_preconditioning ? 1 : 0); } }
        public short ClimateSteeringWheelHeater { get { return (short)(Data.climate_state.steering_wheel_heater ? 1 : 0); } }
        public short ClimateWiperBladeHeater { get { return (short)(Data.climate_state.wiper_blade_heater ? 1 : 0); } }

        #endregion

        #region Drive State

        public long DriveGpsTimestamp { get { return Data.drive_state.gps_as_of; } }
        public short DriveGpsHeading { get { return (short)(Data.drive_state.heading); } }
        public string DriveGpsLatitude { get { return Data.drive_state.latitude.ToString(); } }
        public string DriveGpsLongitude { get { return Data.drive_state.longitude.ToString(); } }
        public string DriveGpsNativeLatitude { get { return Data.drive_state.native_latitude.ToString(); } }
        public short DriveGpsNativeLocationSupported { get { return (short)(Data.drive_state.native_location_supported ? 1 : 0); } }
        public string DriveGpsNativeLongitude { get { return Data.drive_state.native_longitude.ToString(); } }
        public string DriveGpsNativeType { get { return Data.drive_state.native_type; } }
        public short DrivePowerUsage { get { return (short)(Data.drive_state.power); } }
        public string DriveShifterStatus { get { return Data.drive_state.shift_state; } }
        public string DriveCurrentSpeed { get { return Data.drive_state.speed.Value.ToString(); } }

        #endregion

        #region GUI Settings

        public short Settings24HourTime { get { return (short)(Data.gui_settings.gui_24_hour_time ? 1 : 0); } }
        public string SettingsChargeRateUnits { get { return Data.gui_settings.gui_charge_rate_units; } }
        public string SettingsDistanceUnits { get { return Data.gui_settings.gui_distance_units; } }
        public string SettingsRangeDisplay { get { return Data.gui_settings.gui_range_display; } }
        public string SettingsTempUnits { get { return Data.gui_settings.gui_temperature_units; } }

        #endregion 

        #region Vehicle Config

        public short ConfigCanAcceptNavRequests { get { return (short)(Data.vehicle_config.can_accept_navigation_requests ? 1 : 0); } }
        public short ConfigCanActuateTrunks { get { return (short)(Data.vehicle_config.can_actuate_trunks ? 1 : 0); } }
        public string ConfigCarTrim { get { return Data.vehicle_config.car_special_type; } }
        public string ConfigCarModel { get { return Data.vehicle_config.car_type; } }
        public string ConfigExteriorColor { get { return Data.vehicle_config.exterior_color; } }
        public short ConfigHasAirSuspension { get { return (short)(Data.vehicle_config.has_air_suspension ? 1 : 0); } }
        public short ConfigHasLudicrousMode { get { return (short)(Data.vehicle_config.has_ludicrous_mode ? 1 : 0); } }
        public short ConfigHasMotorizedChargePort { get { return (short)(Data.vehicle_config.motorized_charge_port ? 1 : 0); } }
        public string ConfigPerformanceModelStatus { get { return Data.vehicle_config.perf_config; } }
        public short ConfigHasPowerLiftGate 
        { 
            get 
            { 
                return (short)(Data.vehicle_config.plg.HasValue ? (Data.vehicle_config.plg.Value ? 1 : 0) : 0); 
            } 
        }
        public short ConfigHasRearSeatHeaters { get { return (short)Data.vehicle_config.rear_seat_heaters; } }
        public string ConfigRearSeatType { get { return Data.vehicle_config.rear_seat_type; } }
        public short ConfigIsRightHandDrive { get { return (short)(Data.vehicle_config.rhd ? 1 : 0); } }
        public string ConfigRoofColorOrType { get { return Data.vehicle_config.roof_color; } }
        public string ConfigSeatType { get { return Data.vehicle_config.seat_type; } }
        public string ConfigSpoilerType { get { return Data.vehicle_config.spoiler_type; } }
        public string ConfigSunroofInstalled { get { return Data.vehicle_config.sun_roof_installed; } }
        public string ConfigThirdRowSeats { get { return Data.vehicle_config.third_row_seats; } }
        public string ConfigWheelType { get { return Data.vehicle_config.wheel_type; } }

        #endregion

        #region Vehicle State

        public string VehicleAutoParkStatus { get { return Data.vehicle_state.autopark_state_v3; } }
        public string VehicleAutoParkStyle { get { return Data.vehicle_state.autopark_style; } }
        public short VehicleCalendarSupported { get { return (short)(Data.vehicle_state.calendar_supported ? 1 : 0); } }
        public string VehicleSoftwareVersion { get { return Data.vehicle_state.car_version; } }
        public short VehicleDoorsLocked { get { return (Data.vehicle_state.locked ? (short)1 : (short)0); } }

        #endregion

        #endregion

        #region Methods


        #endregion

    }
}