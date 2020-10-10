using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crestron.SimplSharp;

using SimplTeslaCar.Tokens;
using SimplTeslaCar.Models;
using SimplTeslaCar.Results.GetResults;
using SimplTeslaCar.Results.PostResults;

namespace SimplTeslaCar
{
    /// <summary>
    /// The Main class that the Simpl+ module communicates with
    /// </summary>
    public class Main
    {
        public Main()
        {
            cars = new VehicleListModel();
        }

        /// <summary>
        /// Gets or Sets the authorization token for the API
        /// </summary>
        public Token Token
        {
            get { return Settings.Token; }
            private set { Settings.Token = value; }
        }

        private VehicleListModel cars;
        /// <summary>
        /// Gets the list of vehicles associated with the user's account
        /// </summary>
        public VehicleListModel Cars
        {
            get { return cars; }
        }

        /// <summary>
        /// Gets or Sets the ID of the car this module will communicate with
        /// </summary>
        public string CarId
        {
            get { return Settings.CarId; }
            set { Settings.CarId = value; }
        }

        private VehicleSummaryModel summary;
        /// <summary>
        /// Gets the summary information of the car this module communicates with
        /// (NOTE: this action can be used without waking the car.  Getting this information has no battery cost)
        /// </summary>
        public VehicleSummaryModel Summary
        {
            get { return summary; }
        }

        private VehicleDetailsModel details;
        /// <summary>
        /// Gets detailed information regarding the conntected car
        /// (NOTE: this request requires waking the car, and will have some affect on the battery)
        /// </summary>
        public VehicleDetailsModel Details
        {
            get { return details; }
        }

        /// <summary>
        /// Sets the authorization token object's values
        /// </summary>
        /// <param name="token">The token received from Tesla's API</param>
        /// <param name="refreshToken">The refresh token used to request a new token without login credentials</param>
        /// <param name="createdDate">The date when the token was generated</param>
        /// <param name="expiresDate">The date when the token expires</param>
        public void SetToken(
            string token,
            string refreshToken,
            string createdDate,
            string expiresDate)
        {
            Token = new Token();

            Token.access_token = token;
            Token.refresh_token = refreshToken;
            Token.SetCreation(createdDate);

            Settings.TokenInitialized = true;
        }

        /// <summary>
        /// Refresh the car list
        /// </summary>
        private void RefreshCars()
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
                CrestronConsole.PrintLine("SimplTeslaCar.Main.RefreshCars()::Error Refreshing Cars: " + ex.ToString());
            }
        }

        /// <summary>
        /// Requests a summary of the selected car from the API (NOTE: this does not require the car to be woken, and will not affect the battery)
        /// </summary>
        public void GetCarSummary()
        {
            if (cars == null)
            {
                cars = new VehicleListModel();
            }

            try
            {
                cars.RefreshData();

                summary = (from c in cars.Vehicles where c.Id == Settings.CarId select c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.GetCarSummar()::Could not get car information: " + ex.ToString());
            }
        }

        /// <summary>
        /// Requests detailed information of the selected car
        /// </summary>
        public void GetCarDetails()
        {
            try
            {
                VehicleGetDetailsResult vehicleResult = new VehicleGetDetailsResult(CarId);

                vehicleResult.GetResult();
                details = new VehicleDetailsModel(vehicleResult.Data);
                Settings.TemperatureUnits = details.SettingsTempUnits;

            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.GetCarDetails()::Error getting car details: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Battery Charge Level of the car
        /// </summary>
        /// <param name="desiredLevelPercent">The level to which chargin should be set</param>
        public void SetChargeLevel(int desiredLevelPercent)
        {
            try
            {
                ChargeSetLevelResult result = new ChargeSetLevelResult(desiredLevelPercent);
                
                result.GetResult();

                
                if (result.Result.response.result != "true")
                    CrestronConsole.PrintLine("SimplTeslaCar.Main.SetChargeLevel()::Failed: " + result.Result.response.reason);
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.SetChargeLevel()::Error sending charge amount: " + ex.ToString());
            }
        }

        /// <summary>
        /// Requests that the car start charging the battery
        /// </summary>
        public void ChargerStart()
        {
            try
            {
                ChargeControlResult result = new ChargeControlResult(ChargeControlResult.ChargeStates.Start);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.ChargerStart()::Error starting charger: " + ex.ToString());
            }
        }

        /// <summary>
        /// Requests that the car stop charging the battery
        /// </summary>
        public void ChargerStop()
        {
            try
            {
                ChargeControlResult result = new ChargeControlResult(ChargeControlResult.ChargeStates.Stop);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.ChargerStop()::Error stopping charger: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the termerature for the driver's climate control
        /// </summary>
        /// <param name="desiredTemperature">The value to which the climate control should be set</param>
        public void HvacSetDriverLevel(int desiredTemperature)
        {
            decimal convertedLevel;
            HvacSetLevelResult result;
            string rounded;

            try
            {
                HvacSetTempUnits();

                if (Settings.TemperatureUnits == "F")
                {
                    rounded = Calculators.GetTempCelsiusFromFarenheit(Convert.ToDecimal(desiredTemperature)).ToString("0.##");
                }
                else
                {
                    rounded = desiredTemperature.ToString();
                }

                convertedLevel = Convert.ToDecimal(rounded);
                result = new HvacSetLevelResult(convertedLevel, HvacSetLevelResult.TempLocations.Driver);
                result.GetResult();
                CrestronEnvironment.Sleep(2000);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetDriverLevel()::Error Setting Driver HVAC: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the temperature for the passenger's climate control
        /// </summary>
        /// <param name="desiredTemperature">The value to which the climate control should be set</param>
        public void HvacSetPassengerLevel(int desiredTemperature)
        {
            decimal convertedLevel;
            HvacSetLevelResult result;
            string rounded;

            try
            {
                HvacSetTempUnits();

                if (Settings.TemperatureUnits == "F")
                {
                    rounded = Calculators.GetTempCelsiusFromFarenheit(Convert.ToDecimal(desiredTemperature)).ToString("0.##");
                }
                else
                {
                    rounded = desiredTemperature.ToString();
                }

                convertedLevel = Convert.ToDecimal(rounded);
                result = new HvacSetLevelResult(convertedLevel, HvacSetLevelResult.TempLocations.Passenger);
                result.GetResult();
                CrestronEnvironment.Sleep(2000);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetPassengerLevel()::Error Setting Passenger HVAC: " + ex.ToString());
            }
        }

        /// <summary>
        /// Gets the climate control's set value, based on the user's preferred units as set in the car's UI
        /// </summary>
        /// <param name="desiredTemperature">The value to be converted</param>
        private decimal HvacGetConvertedLevel(int desiredTemperature)
        {
            decimal result;

            HvacSetTempUnits();

            result = Convert.ToDecimal(Calculators.GetTemperatureValue(desiredTemperature, Settings.TemperatureUnits));

            return result;
        }

        /// <summary>
        /// Sets the user's preferred units for temperature (celsius (C) or farenheit (F))
        /// </summary>
        private void HvacSetTempUnits()
        {
            if (string.IsNullOrEmpty(Settings.TemperatureUnits))
            {
                if (details == null)
                {
                    GetCarDetails();
                }
                else
                {
                    Settings.TemperatureUnits = details.SettingsTempUnits;
                }
            }
        }

        /// <summary>
        /// Starts the car's climate control
        /// </summary>
        public void HvacStart()
        {
            try
            {
                HvacControlResult result = new HvacControlResult(HvacControlResult.HvacStates.Start);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacStart()::Error sending HVAC Start: " + ex.ToString());
            }
        }

        /// <summary>
        /// Stops the car's climate control
        /// </summary>
        public void HvacStop()
        {
            try
            {
                HvacControlResult result = new HvacControlResult(HvacControlResult.HvacStates.Stop);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacStop()::Error sending HVAC Stop: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Driver Seat Heater to the desired level        
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetDriverSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterFrontLeft, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetDriverSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Passenger Seat Heater to the desired level        
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetPassengerSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterFrontRight, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetPassengerSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Driver's side Seat Heater to the desired level        
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetRearLeftSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterRearLeft, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetRearLeftSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Center Seat Heater to the desired level        
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetRearCenterSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterRearCenter, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetRearCenterSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Passenger Seat Heater to the desired level        
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetRearRightSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterRearRight, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetRearRightSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Left "Back" Seat Heater to the desired level UNTESTED.  Also, it is unclear what the difference is between this and 3rd row, 
        /// but I believe it to be the optional rear facing seats for the Tesla Model S
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetRearLeftBackSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterRearLeftBack, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetRearLeftBackSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Right "Back" Seat Heater to the desired level UNTESTED.  Also, it is unclear what the difference is between this and 3rd row, 
        /// but I believe it to be the optional rear facing seats for the Tesla Model S
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSetRearRightBackSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeaterRearRightBack, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetRearRightBackSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Left 3rd Row Seat Heater to the desired level UNTESTED.  Also, it is unclear what the difference is between this and 3rd row, 
        /// but I believe it to be the optional 3rd Row seats for the Tesla Model X
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSet3rdRowLeftSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeater3rdRowLeft, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSet3rdRowLeftSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the Rear Left 3rd Row Seat Heater to the desired level UNTESTED.  Also, it is unclear what the difference is between this and 3rd row, 
        /// but I believe it to be the optional 3rd Row seats for the Tesla Model X
        /// </summary>
        /// <param name="level">0 = off; 1 = low; 2 = medium; 3 = high</param>
        public void HvacSet3rdRowRightSeatHeater(int level)
        {
            try
            {
                HvacSetSeatHeaterResult result = new HvacSetSeatHeaterResult(HvacSetSeatHeaterResult.SeatHeaterPositions.SeatHeater3rdRowRight, level);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSet3rdRowRightSeatHeater()::Error sending command: " + ex.ToString());
            }
        }

        /// <summary>
        /// Sets the steering wheel heater on or off
        /// </summary>
        /// <param name="turnOn">0 = turn off, all other values = turn on</param>
        public void HvacSetSteeringWheelHeater(int turnOn)
        {
            try
            {
                HvacSetSteeringWheelHeaterResult result = new HvacSetSteeringWheelHeaterResult(turnOn);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.HvacSetSteeringWheelHeater()::Error sending commmand: " + ex.ToString());
            }
        }


        /// <summary>
        /// Lock's the car's doors
        /// </summary>
        public void DoorLock()
        {
            try
            {
                DoorLockResults result = new DoorLockResults(DoorLockResults.LockStates.Locked);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.DoorLock()::Error locking doors: " + ex.ToString());
            }
        }

        /// <summary>
        /// Unlocks the car's doors
        /// </summary>
        public void DoorUnlock()
        {
            try
            {
                DoorLockResults result = new DoorLockResults(DoorLockResults.LockStates.Unlocked);
                result.GetResult();
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.DoorUnlock()::Error unlocking doors: " + ex.ToString());
            }
        }

        /// <summary>
        /// Wakes the car
        /// </summary>
        public string WakeCar()
        {
            try
            {
                WakePostResult wakeResult = WakePostResult.RequestWake(CarId);
                SimplTeslaCar.Data.VehicleSummaryData data = wakeResult.Result.response;
                return data.state;
            }
            catch (Exception ex)
            {
                CrestronConsole.PrintLine("SimplTeslaCar.Main.WakeCar()::Error waking car: " + ex.ToString());
                return "Unable to wake car";
            }
        }
    }
}