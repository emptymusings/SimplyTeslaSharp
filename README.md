# SimplyTeslaSharp
Simpl# libraries used in order to connect the Crestron Series 3 home automation system to a Tesla Vehicle

## Requirements:
* Crestron Simpl Windows application
* Crestron Series 3 processor

### Development Requirements (optional)
It is assumed if you're interested in this project, that you already have a knowledge of Simpl# and have an environment set up for development purposes or have access to gather the necessary information from Crestron to do so.

Because these development requirements are no longer supported by Microsoft, I've included the MSIs in the `assets` folder.  These files are necessary to develop in the Compact Framework environment.  
* .Net 3.5 Compact Framework
* .Net Compact Framework PowerToys

**NOTE**: Keeping these msi files will be handy, as I've noted a number of times wherein MS updates will delete the necessary targets file, resulting in the following error:
```
The imported project "C:\Windows\Microsoft.NET\Framework\v3.5\Microsoft.CompactFramework.CSharp.targets" was not found. Confirm that the path in the declaration is correct, and that the file exists on disk.
```
You may also be able to find these downloads using the Wayback machine (i.e. .NETCFPowerToys can be found by navigating to Sept. 30, 2014 [here](https://web.archive.org/web/20140901000000*/https://download.microsoft.com/download/f/a/c/fac1342d-044d-4d88-ae97-d278ef697064/NETCFv35PowerToys.msi))
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
| Climate_Set_Driver_Seat_Heater_Level<br>Climate_Set_Rear_Center_Seat_Heater_Level<br>Climate_Set_Rear_Left_Back_Seat_Heater_Level<br>Climate_Set_Rear_Right_Seat_Heater_Level<br>Climate_Set_Rear_Right_Back_Seat_Heater_Level<br>Climate_Set_Passenger_Seat_Heater_Level | Integer | Sets the seat heater to the level assigned:<br>0 = off<br>1 = low<br>2 = medium<br>3 = high<br>**NOTE:** Due to logic in Tesla's API, these calls will not function **unless the Climate Control is turned on** |
| Climate_Set_Steering_Wheel_Heater_Level | Integer | Turns the Steering Wheel Heater on/off (on = 1, off = 2) **UNTESTED** |
| Door_Lock | Digital | Locks all of the vehicle's doors |
| Door_Unlock | Digital | Unlocks all of the vehicle's doors |
| Car_Wake_Up | Digital | Wakes the vehicle up if it is sleeping, might be useful if commands are timing out |
