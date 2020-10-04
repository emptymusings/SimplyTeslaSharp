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

1) Download the SIMPL+ and SIMPL modules in the [UsrsPlus.zip file](https://github.com/lamentary/SimplyTeslaSharp/raw/main/UsrsPlus.zip) 

2) Extract the UsrsPlus.zip file's contents to SIMPL Windows' location for User SIMPL+ modules.  To find this location, open SIMPL Windows, click on **Options -> Preferences** in the menu to open the Preferences Dialog, then select the **Directories** tab.

3) Close and reopen SIMPL Windows.  You should now be able to find the *Tesla Master Processor* and *Tesla Car Processor* modules under in the **Symbol Library** under **User Modules -> Vehicles**
