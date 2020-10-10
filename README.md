# SimplyTeslaSharp
Simpl# libraries used in order to connect the Crestron Series 3 home automation system to a Tesla Vehicle

## Requirements:
* Crestron Simpl Windows application
* Crestron Series 3 processor

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
| Is_Logged_In | Digital | Value that identifies whether the module has authenticated with Tesla's API (0 = false, 1 = true) |
| Car_Count | Integer | The number of cars associated with the user's Tesla account |
| Login_Token_FB | String | The authorization bearer token used to make any requests with the API |
| Login_Token_Created_FB | String | The date and time that the authorizationn token was created (all times are GMT) |
| Login_Token_Expires_FB | String | The date and time that the authorization token will/has expired (all times are GMT) |
| Login_Refresh_Token_FB | String | The token to be used in order to refresh/regenerate the authorization token.  This makes automatically refreshing when it expires possible |
| Car_Id\[1-5] | String | The Tesla assigned ID value for a vehicle, used to route requests.  Currently, the Tesla Master Processor supports up to 5 cars |

