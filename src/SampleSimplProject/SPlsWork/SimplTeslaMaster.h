namespace SimplTeslaMaster;
        // class declarations
         class VehicleSummaryModel;
         class VehicleListModel;
         class Main;
         class Authentication;
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

     class Main 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION RefreshCars ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        Authentication Auth;
        VehicleListModel Cars;
        SIGNED_INTEGER CarCount;
    };

     class Authentication 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION Initialize ( STRING username , STRING password );
        FUNCTION SendTokenRequest ();
        STRING_FUNCTION GetCurrentToken ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING UserName[];
        STRING Password[];
        STRING TokenCreated[];
        STRING TokenExpires[];
        STRING RefreshToken[];
    };

