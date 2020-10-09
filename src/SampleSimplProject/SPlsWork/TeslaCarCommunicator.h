namespace SimplTeslaCar.Models;
        // class declarations
         class VehicleListModel;
         class CommandResultModel;
         class VehicleDetailsModel;
         class VehicleSummaryModel;
         class ChargingStateModel;
     class VehicleListModel 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION RefreshData ();
        STRING_FUNCTION GetIdFromName ( STRING vehicleName );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER VehicleCount;
        VehicleSummaryModel Vehicles[];
    };

     class CommandResultModel 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING reason[];
        STRING result[];
    };

     class VehicleDetailsModel 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Name[];
        STRING Status[];
        SIGNED_INTEGER InService;
        STRING Id[];
        SIGNED_INTEGER CalendarEnabled;
        STRING Vin[];
        SIGNED_INTEGER BatteryLevel;
        STRING BatteryRange[];
        SIGNED_INTEGER BatteryChargeAmpsUserDefined;
        SIGNED_INTEGER BatteryChargeAmpsMax;
        SIGNED_INTEGER BatteryChargeEnableRequest;
        STRING BatteryLastChargeEnergyAddedKwh[];
        STRING BatteryChargeLimitValue[];
        STRING BatteryLastChargeMilesAdded[];
        SIGNED_INTEGER BatteryChargePortDoorOpen;
        STRING BatteryChargePortLatchStatus[];
        STRING BatteryChargeRate[];
        SIGNED_INTEGER BatteryChargeToMaxRange;
        STRING BatteryChargeActualCurrent[];
        SIGNED_INTEGER BatteryChargerPhases;
        SIGNED_INTEGER BatteryChargerPilotCurrentAmps;
        SIGNED_INTEGER BatteryChargerPowerKwh;
        SIGNED_INTEGER BatteryChargerVoltage;
        STRING BatteryChargingStatus[];
        STRING BatteryEstimatedRange[];
        STRING BatteryIdealRange[];
        SIGNED_INTEGER BatteryScheduledChargePending;
        STRING BatteryScheduledChargeStartTime[];
        STRING BatteryHoursToFullCharge[];
        SIGNED_INTEGER BatteryIsChargingForTrip;
        SIGNED_INTEGER BatteryUsableChargeLevel;
        SIGNED_INTEGER BatteryHeater;
        SIGNED_INTEGER BatteryHeaterNoPower;
        STRING ClimateDriverTempSetting[];
        SIGNED_INTEGER ClimateFanLevel;
        STRING ClimateInsideTemp[];
        SIGNED_INTEGER ClimateIsAutoConditioningOn;
        SIGNED_INTEGER ClimateIsHvacOn;
        SIGNED_INTEGER ClimateIsFrontDefrosterOn;
        SIGNED_INTEGER ClimateIsPreconditioning;
        SIGNED_INTEGER ClimateIsRearDefrosterOn;
        SIGNED_INTEGER ClimateLeftVentDirection;
        STRING ClimateAvailableTempMax[];
        STRING ClimateAvailableTempMin[];
        STRING ClimateOutsideTemp[];
        STRING ClimatePassengerTempSetting[];
        SIGNED_INTEGER ClimateRghtVentDirection;
        SIGNED_INTEGER ClimateSeatHeaterDriver;
        SIGNED_INTEGER ClimateSeatHeaterRearCenter;
        SIGNED_INTEGER ClimateSeatHeaterRearLeft;
        SIGNED_INTEGER ClimateSeatHeaterRearLeftBack;
        SIGNED_INTEGER ClimateSeatHeaterRearRight;
        SIGNED_INTEGER ClimateSeatHeaterRearRightBack;
        SIGNED_INTEGER ClimateSeatHeaterPassenger;
        SIGNED_INTEGER ClimateSideMirrorHeaters;
        SIGNED_INTEGER ClimateSmartPreconditioning;
        SIGNED_INTEGER ClimateSteeringWheelHeater;
        SIGNED_INTEGER ClimateWiperBladeHeater;
        SIGNED_INTEGER DriveGpsHeading;
        STRING DriveGpsLatitude[];
        STRING DriveGpsLongitude[];
        STRING DriveGpsNativeLatitude[];
        SIGNED_INTEGER DriveGpsNativeLocationSupported;
        STRING DriveGpsNativeLongitude[];
        STRING DriveGpsNativeType[];
        SIGNED_INTEGER DrivePowerUsage;
        STRING DriveShifterStatus[];
        STRING DriveCurrentSpeed[];
        SIGNED_INTEGER Settings24HourTime;
        STRING SettingsChargeRateUnits[];
        STRING SettingsDistanceUnits[];
        STRING SettingsRangeDisplay[];
        STRING SettingsTempUnits[];
        SIGNED_INTEGER ConfigCanAcceptNavRequests;
        SIGNED_INTEGER ConfigCanActuateTrunks;
        STRING ConfigCarTrim[];
        STRING ConfigCarModel[];
        STRING ConfigExteriorColor[];
        SIGNED_INTEGER ConfigHasAirSuspension;
        SIGNED_INTEGER ConfigHasLudicrousMode;
        SIGNED_INTEGER ConfigHasMotorizedChargePort;
        STRING ConfigPerformanceModelStatus[];
        SIGNED_INTEGER ConfigHasPowerLiftGate;
        SIGNED_INTEGER ConfigHasRearSeatHeaters;
        STRING ConfigRearSeatType[];
        SIGNED_INTEGER ConfigIsRightHandDrive;
        STRING ConfigRoofColorOrType[];
        STRING ConfigSeatType[];
        STRING ConfigSpoilerType[];
        STRING ConfigSunroofInstalled[];
        STRING ConfigThirdRowSeats[];
        STRING ConfigWheelType[];
        STRING VehicleAutoParkStatus[];
        STRING VehicleAutoParkStyle[];
        SIGNED_INTEGER VehicleCalendarSupported;
        STRING VehicleSoftwareVersion[];
        SIGNED_INTEGER VehicleDoorsLocked;
    };

     class VehicleSummaryModel 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION RefreshData ( STRING idString );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING Name[];
        STRING Status[];
        SIGNED_INTEGER InService;
        STRING Id[];
        SIGNED_LONG_INTEGER CalendarEnabled;
        STRING Vin[];
    };

     class ChargingStateModel 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION RefreshData ( STRING vehicleIdString );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING ChargingStatus[];
        STRING FastChargerType[];
        STRING FastChargerBrand[];
        SIGNED_LONG_INTEGER ChargeLimitSoc;
        SIGNED_LONG_INTEGER ChargeLimitSocStd;
        SIGNED_LONG_INTEGER ChargeLimitSocMin;
        SIGNED_LONG_INTEGER ChargeLimitSocMax;
        SIGNED_LONG_INTEGER ChargeToMaxRange;
        SIGNED_LONG_INTEGER MaxRageChargeCounter;
        SIGNED_LONG_INTEGER FastChargerPresent;
        STRING BatteryRange[];
        STRING EstimatedBatteryRange[];
        STRING IdealBatteryRange[];
        SIGNED_LONG_INTEGER BatteryLevel;
        STRING ChargeEnergyAdded[];
        STRING ChargeMilesAddedRated[];
        STRING ChargeMilesAddedIdeal[];
        STRING ChargerVoltage[];
        STRING ChargerPilotCurrent[];
        STRING ChargerActualCurrent[];
        STRING ChargerPower[];
        STRING TimeToFullCharge[];
        SIGNED_LONG_INTEGER TripCharging;
        STRING ChargeRate[];
        SIGNED_LONG_INTEGER ChargePortDoorOpen;
        STRING ConnectedChargingCable[];
        STRING ScheduledChargingTime[];
        SIGNED_LONG_INTEGER ScheduledChargingPending;
        SIGNED_LONG_INTEGER ChargeEnableRequest;
        STRING ChargePortLatch[];
        STRING ChargeCurrentRequest[];
        STRING ChargeCurrentRequestMax[];
        SIGNED_LONG_INTEGER ManagedChargingActive;
        SIGNED_LONG_INTEGER ManagedChargingUserCanceled;
        STRING ManagedChargingStartTime[];
        SIGNED_LONG_INTEGER BatteryHeaterOn;
    };

namespace SimplTeslaCar.Results.PostResults;
        // class declarations
         class HvacStates;
         class TempLocations;
         class LockStates;
         class ChargeStates;
         class SeatHeaterPositions;
    static class HvacStates // enum
    {
        static SIGNED_LONG_INTEGER Start;
        static SIGNED_LONG_INTEGER Stop;
    };

    static class TempLocations // enum
    {
        static SIGNED_LONG_INTEGER Driver;
        static SIGNED_LONG_INTEGER Passenger;
    };

    static class LockStates // enum
    {
        static SIGNED_LONG_INTEGER Locked;
        static SIGNED_LONG_INTEGER Unlocked;
    };

    static class ChargeStates // enum
    {
        static SIGNED_LONG_INTEGER Start;
        static SIGNED_LONG_INTEGER Stop;
    };

    static class SeatHeaterPositions // enum
    {
        static SIGNED_LONG_INTEGER SeatHeaterFrontLeft;
        static SIGNED_LONG_INTEGER SeatHeaterFrontRight;
        static SIGNED_LONG_INTEGER SeatHeaterRearLeft;
        static SIGNED_LONG_INTEGER SeatHeaterRearLeftBack;
        static SIGNED_LONG_INTEGER SeatHeaterRearCenter;
        static SIGNED_LONG_INTEGER SeatHeaterRearRight;
        static SIGNED_LONG_INTEGER SeatHeaterRearRightBack;
        static SIGNED_LONG_INTEGER SeatHeater3rdRowLeft;
        static SIGNED_LONG_INTEGER SeatHeater3rdRowRight;
    };

namespace SimplTeslaCar;
        // class declarations
         class Main;
         class CommandUrls;
           class Token;
     class Main 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION SetToken ( STRING token , STRING refreshToken , STRING createdDate , STRING expiresDate );
        FUNCTION GetCarSummary ();
        FUNCTION GetCarDetails ();
        FUNCTION SetChargeLevel ( SIGNED_LONG_INTEGER desiredLevelPercent );
        FUNCTION ChargerStart ();
        FUNCTION ChargerStop ();
        FUNCTION HvacSetDriverLevel ( SIGNED_LONG_INTEGER desiredTemperature );
        FUNCTION HvacSetPassengerLevel ( SIGNED_LONG_INTEGER desiredTemperature );
        FUNCTION HvacStart ();
        FUNCTION HvacStop ();
        FUNCTION HvacSetDriverSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetPassengerSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetRearLeftSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetRearCenterSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetRearRightSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetRearLeftBackSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetRearRightBackSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSet3rdRowLeftSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSet3rdRowRightSeatHeater ( SIGNED_LONG_INTEGER level );
        FUNCTION HvacSetSteeringWheelHeater ( SIGNED_LONG_INTEGER turnOn );
        FUNCTION DoorLock ();
        FUNCTION DoorUnlock ();
        STRING_FUNCTION WakeCar ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        Token Token;
        VehicleListModel Cars;
        STRING CarId[];
        VehicleSummaryModel Summary;
        VehicleDetailsModel Details;
    };

     class CommandUrls 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING ChargeSetBatteryLevel[];
        STRING ChargeStart[];
        STRING ChargeStop[];
        STRING DoorLock[];
        STRING DoorUnlock[];
        STRING HvacSetTemp[];
        STRING HvacStart[];
        STRING HvacStop[];
        STRING HvacSetSeatHeater[];
        STRING HvacSetSteeringWheelHeater[];
    };

namespace SimplTeslaCar.Tokens;
        // class declarations
         class Token;
     class Token 
    {
        // class delegates

        // class events
        EventHandler TokenChanged ( Token sender, EventArgs e );

        // class functions
        FUNCTION SetCreation ( STRING creationDateString );
        FUNCTION SetExpiration ( STRING expirationDateString );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING access_token[];
        STRING token_type[];
        STRING refresh_token[];
        STRING CreatedTimeString[];
        STRING ExpiresString[];
    };

namespace SimplTeslaCar.WebRequests;
        // class declarations
         class ContentFactory;
     class ContentFactory 
    {
        // class delegates

        // class events

        // class functions
        static STRING_FUNCTION SetFormContentDisposition ( STRING fieldName , STRING value );
        static STRING_FUNCTION FinalizeFormDisposition ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

