# SimplyTeslaSharp
Simpl# libraries used in order to connect the Crestron Series 3 home automation system to a Tesla Vehicle

# IMPORTANT
As of the end of January, 2021, Tesla has changed their server and method for authenticating users.  As it stands, this application will no longer work.  Progress is being made on updating the authorization token path, and handling the addition of MFA, but **consider this repository on hold** until a solution is found.

More progress can be found at https://tesla-api.timdorr.com/api-basics/authentication

## Requirements:
* Crestron Simpl Windows application
* Crestron Series 3 processor (while the compile process works for the Series 4 processor, I do not have one to test on, and therefore cannot verify compatibility)

## Prerequisites:
* A working knowledge of Crestron SIMPL Windows.  
* No knowledge of SIMPL+ or SIMPL# is necessary, but it could help.

This library is tested using a Crestron MC-3 and a Tesla Model 3.  Tesla's API should, in theory, work with any model of Tesla vehicle, though I have not had the opportunity to try yet.  Additionally, there are some options available from the API for features which my vehicle does not have, so those are untested (such as 3rd row seat heaters).

## Usage
*NOTE: I have only used SIMPL Windows for this, and cannot verify if, or offer instructions for how, it will work in any other application*

1) Download the SIMPL+ and SIMPL modules in the [UserPackages.zip file](https://github.com/lamentary/SimplyTeslaSharp/raw/main/assets/UserPackages.zip) 

2) Extract the UserPackages.zip file's contents. Copy the contents of the Usrsplus and Usrmacro folders to the appropriate folders in Crestron's SIMPL folder. To find this location, open SIMPL Windows, click on **Options -> Preferences** in the menu to open the Preferences Dialog, then select the **Directories** tab.

3) Close and reopen SIMPL Windows.  You should now be able to find the **Tesla Master Processor** and **Tesla Car Processor** modules under in the **Symbol Library** under **User Modules -> Vehicles**

You can take a look at the quick sample project [here](https://github.com/lamentary/SimplyTeslaSharp/raw/main/assets/SampleSimplProject.zip)

**Any project that utilizes these libraries will require 1 Tesla Master Processor module per user account (I would assume 1, but there might be multiple in one deployment), and 1 Tesla Car Processor module per vehicle.**

### Tesla Master Processor
This module is predominantly used for authentication and authorization, as well as token management, in order to utilize the Tesla API.  As with Tesla's web site and mobile app, access to the API will require login using the user's email and password.  If the user has multiple vehicles, their vehicle IDs (up to 5) will be tracked here, as well.  The Tesla Car Processor module depends on this module being used

#### Inputs
| Signal Name | Signal Type | Description |
| ----------- | ----------- | ----------- |
| Request_Login_Token | Digital | Sends the user's login information to Tesla's authentication site to receive a token that is needed for any further requests to the API's to succeed |
| Request_Car_List | Digital | Requests a list of all vehicles associated with the user's Tesla account |
| Login_Email | String | The user's email that is registered as their user name with Tesla |
| Login_Password | String | The user's password that is registered with Tesla |

#### Outputs
| Signal Name | Signal Type | Description |
| ----------- | ----------- | ----------- |
| Is_Logged_In | Digital | Value that identifies whether the module has authenticated with Tesla's API |
| Car_Count | Integer | The number of cars associated with the user's Tesla account |
| Login_Token_FB | String | The authorization bearer token used to make any requests with the API |
| Login_Token_Created_FB | String | The date and time that the authorization token was created (all times are GMT) |
| Login_Token_Expires_FB | String | The date and time that the authorization token will/has expired (all times are GMT) |
| Login_Refresh_Token_FB | String | The token to be used in order to refresh/regenerate the authorization token. |
| Car_Id\[1-5] | String | The Tesla assigned ID value for a vehicle, used to route requests.  Currently, the Tesla Master Processor supports up to 5 cars |

### Tesla Car Processor
This module is used issue commands and gather information specific to a single vehicle.  It should be noted that most commands will "wake" the vehicle, and if set on a timed polling schedule might affect battery charge (and therefor range) if sent frequently.  Commands that do not wake the vehicle will be noted.

### Inputs
| Signal Name | Signal Type | Description |
| ----------- | ----------- | ----------- |
| Token | String | The authorization token obtained when logging in (should be linked to the associated Tesla Master Processor's Login_Token_FB signal) |
| Refresh_Token | String | The authorization refresh token (should be linked to the associated Tesla Master Processor's Login_Refresh_Token_FB signal) |
| Token_Created_Date | String | The date and time that the authorization token was generated (should be linked to the Tesla Master Processor's Login_Token_Created_FB signal) |
| Token_Expiration_Date | String | The date and time that the current active token will/has expired (should be linked to the associated Tesla Master Processor's Login_Token_Expires_FB signal) |
| Car_ID | String | The Tesla assigned ID value for the vehicle, used to route requests (should be linked to one of the Tesla Master Processor's Car_Id signals) |
| Get_Car_Summary | Digital | Requests summary information from the associated vehicle (**NOTE:** this request does not wake car and should have no impact on battery range) |
| Set_Charge_Level | Integer | Sets the charge level limit to the value indicated (50-100%) |
| Battery_Charging_Start | Digital | Sends a command to the associated vehicle to start charging the drive battery (if the preset charge limit has not been reached) |
| Battery_Charging_Stop | Digital | Sends a command to the associated vehicle to stop charging the drive battery |
| Climate_Turn_Hvac_On | Digital | Turns the vehicle's climate control on |
| Climate_Turn_Hvac_Off | Digital | Turns the vehicle's climate control off |
| Climate_Set_Driver_Temp | Integer | Sets the isolated climate control temperature for the driver's side to the entered value |
| Climate_Set_Passenger_Temp | Integer | Sets the isolated climate control temperature for the passenger's side to the entered value |
| Climate_Set_Driver_Seat_Heater_Level<br>Climate_Set_Rear_Center_Seat_Heater_Level<br>Climate_Set_Rear_Left_Back_Seat_Heater_Level<br>Climate_Set_Rear_Right_Seat_Heater_Level<br>Climate_Set_Rear_Right_Back_Seat_Heater_Level<br>Climate_Set_Passenger_Seat_Heater_Level | Integer | Sets the seat heater to the level assigned:<br>0 = off<br>1 = low<br>2 = medium<br>3 = high<br><br>**NOTE:** Due to logic in Tesla's API, these calls will not function **unless the Climate Control is turned on** |
| Climate_Set_Steering_Wheel_Heater_Level | Integer | Turns the Steering Wheel Heater on/off (on = 1, off = 2) **UNTESTED** |
| Door_Lock | Digital | Locks all of the vehicle's doors |
| Door_Unlock | Digital | Unlocks all of the vehicle's doors |
| Car_Wake_Up | Digital | Wakes the vehicle up if it is sleeping, might be useful if commands are timing out |

### Outputs
| Signal Name | Signal Type | Description |
| ----------- | ----------- | ----------- |
| Car_Name | String | The name of the vehicle that the user has set |
| Car_Status | String | Description of the car's state in terms of power-saving mode.  Possible results:<br>asleep - power saving mode<br>waking - a request has been sent, but the car was asleep, and cannot report the results until it's online<br>online |
| Car_In_Service | Integer | Boolean value (0d or 1d) showing whether the vehicle is currently being drive |
| Car_Vin | String | The vehicle's VIN |
| Battery_Level | Integer | The vehicle's current level of battery remaining (0-100%) |
| Battery_Charge_Amps_UserDefined | Integer | The user defined amperage to charge the vehicle at (set via the vehicle's touchscreen) |
| Battery_Charge_Amps_Max | Integer | The maximum of amperage that the vehicle is able to charge at, based on the onboard charger's maximum amperage |
| Battery_Charge_Enable_Request | Integer | (Unverified, but suspected) Boolean value showing whether charging can be initiated via the API |
| Battery_Charge_Port_Door_Open | Integer | Boolean value that shows whether the vehicle's charge port door is currently open |
| Battery_Charge_to_Max_Range | Integer | Boolean value that shows if the vehicle is set to maximum charge (100%) |
| Battery_Charger_Phases | Integer | Unknown (to me) at this time |
| Battery_Charge_Pilot_Current_Amps | Integer | Appears to be another notation of amperage |
| Battery_Charger_Power_kWh | Integer | The total kWh currently being drawn by the charger |
| Battery_Charger_Voltage | Integer | The total voltage being currently being drawn by the charger |
| Battery_Is_Charging_For_Trip | Integer | Boolean value.  While the name *should* make its usage intuitive, I have not observed this value being **true** yet |
| Battery_Usable_Charge_Level | Integer | Displays the percent of battery available for use |
| Battery_Range | String | Total miles/kilometers (depending on user's settings) estimated to be available at current charge state |
| Battery_Last_Charge_Energy_Added_kWh | String | The total number of kWh added to the battery during the last charging session |
| Battery_Charge_Limit_Value | String | The user entered maximum charge level |
| Battery_Last_Charge_Miles_Added | String | The total miles added to the battery during the last charging session |
| Battery_Charge_Port_Latch_Status | String | I have not observed a value other than "Engaged" - even when the vehicle is unplugged |
| Battery_Charge_Rate | String | The amperage at which the vehicle's battery is charging |
| Battery_Charge_Actual_Current | String | Another metric of the amperage at which the vehicle's battery is charging, rounded to the nearest amps |
| Battery_Charging_Status | String | Values:<br>Starting<br>Charging<br>Complete<br>Disconnected<br>Stopped<br>NoPower |
| Battery_Estimated_Range | String | The estimated number of miles or km that the vehicle is able to drive based on its current charged amount.  I am unsure how Tesla calculates this value vs the ideal range |
| Battery_Ideal_Range | String | The ideal number of miles or km that the vehicle is able to drive based on its current charged amount.  I am unsure how Tesla calculates this value vs the estimated range |
| Battery_Scheduled_Charge_Start_Time | String | The time scheduled to start charging based on user entry in the vehicle's touchscreen |
| Battery_Hours_to_Full_Charge | String | A decimal representation of the total hours left to charge to the user's selected battery level (e.g. 1.25 would be equivalent to 1 hour and 15 minutes) |
| Battery_Heater | String | Boolean value indicating whether the battery heater is on or off |
| Battery_Heater_No_Power | String | Boolean value indicating whether there is sufficient power to operate the battery heater |
| Climate_Fan_Level | Integer | A value ranging from 0-7 indicating the fan level (0 = off, 7 = maximum) |
| Climate_Auto_Conditioning_Is_On | Integer | Boolean value indicating whether automatic climate control/battery beating set to auto or manual |
| Climage_Hvac_Is_On | Integer | Boolean value indicating whether climate control is on or off |
| Climate_Front_Defroster_On | Integer | Boolean value indicating whether the windshield's defroster is on or off |
| Climate_Preconditioning | Integer | Boolean value indicating whether the vehicle is preconditioning |
| Climate_Rear_Defroster_Is_on | Integer | Boolean value indicating whether the rear defroster is on or off |
| Climate_Left_Vent_Direction | Integer | Value indicating the position of the driver-side vent |
| Climate_Right_Vent_Direction | Integer | Value indicating the position of the passenger-side vent |
| Climate_Seat_Heater_Driver<br>Climate_Seat_Heater_Rear_Center<br>Climate_Seat_Heater_Rear_Center<br>Climate_Seat_Heater_Rear_Left<br>Climate_Seat_Heater_Rear_Left_Back<br>Climate_Seat_Heater_Rear_Right<br>Climate_Seat_Heater_Rear_Right_BackClimate_Seat_Heater_Passenger | Integer | The level at which the a seat heater is operating:<br>0 = off<br>1 = low<br>2 = medium<br>3 = high |
| Climate_Side_Mirror_Heaters | Integer | Boolean value indicating whether the vehicle's side mirror heaters are operating |
| Climate_Smart_Preconditioning | Integer | Boolean value indicating whether the vehicle is using smart preconditioning |
| Climate_Steering_Wheel_Heater | Integer | Boolean value indicating whether the steering wheel heater is on or off |
| Climate_Wiper_Blade_Heater | Integer | Boolean value indicating whether the windshield wiper blade heater is on or off |
| Climate_Driver_Temp_Settings | String | The temperate set for the driver-side climate control |
| Climate_Inside_Temp | String | The current interior temperature of the vehicle |
| Climate_Available_Temp_Max | String | The maximum available temperature to which the climate control can be set |
| Climate_Available_Temp_Min | String | The minimum available temperature to which the climate control can be set |
| Climate_Outside_Temp | String | The current temperature outside of the vehicle |
| Climate_Passenger_Temp_Setting | String | The temperate set for the passenger-side climate control |
| Settings_Units_Distance | String | The units used to measure distance (miles/km) |
| Settings_Units_Temperature | String | The units used to measure temperature (F/C) |
| Car_Doors_Locked | Integer | Boolean value indicating whether the vehicle's doors are locked |
| Summary_Refreshed_Date_Time | String | Date/Time value indicating when the summary data was last refreshed |
| Details_Refreshed_Date_Time | String | Date/Time value indicating when the detailed data was last refreshed |
