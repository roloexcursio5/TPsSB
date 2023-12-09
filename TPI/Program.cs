namespace TPI
{
    internal class Program
    {
        static public Barrack Barrack { get; set; }
        static public CommandMenu Menu { get; set; }
               

        static void Main(string[] args)
        {
            DatabaseConection.FillConectionInformation("LENOVO-PC\\SQLEXPRESS", "NewUser", "NU", "TPI");

            (Location[,], Barrack) MB = SaveLoadFunctions.GetMatchStarted("Land","Barrack");
            Map.LatitudLongitud = MB.Item1;
            Barrack = MB.Item2;

            Menu = CommandMenu.GetInstance(Barrack);

            Menu.DisplayMenu();
        }
    }
}



/*
Estructura de la base de datos
 
CREATE TABLE Barrack(
AutoIncrementalID int,
LocationLatitud int,
LocationLongitud int,
OperatorType varchar(20),
OperatorUniqueID int,
OperatorGeneralStatus varchar(50),
OperatorBattery int,
OperatorLoad int,
OperatorLocationLatitud int,
OperatorLocationLongitud int,
OperatorDamages varchar(100));

 
CREATE TABLE Map(
Latitud int,
Longitud int,
LandType varchar(20));
 

Base cargada por fuera del sistema
CREATE TABLE OperatorsActivityLog(
DateRegistration DateTime,
OperatorUniqueID int,
Kilometers int,
BatteryConsumption int,
TransportedCargo int,
Damages int,
LocationLatitud int,
LocationLongitud int);



INSERT INTO OperatorsActivityLog(DateRegistration, OperatorUniqueID, Kilometers, BatteryConsumption, TransportedCargo, Damages, LocationLatitud, LocationLongitud) 
VALUES (CURRENT_TIMESTAMP,0, 10, 20, 30, 5, 22,22);

INSERT INTO OperatorsActivityLog(DateRegistration, OperatorUniqueID, Kilometers, BatteryConsumption, TransportedCargo, Damages, LocationLatitud, LocationLongitud) 
VALUES (CURRENT_TIMESTAMP,0, 11, 21, 31, 6, 23,23);

INSERT INTO OperatorsActivityLog(DateRegistration, OperatorUniqueID, Kilometers, BatteryConsumption, TransportedCargo, Damages, LocationLatitud, LocationLongitud) 
VALUES (CURRENT_TIMESTAMP,0, 12, 22, 32, 7, 24,24);

INSERT INTO OperatorsActivityLog(DateRegistration, OperatorUniqueID, Kilometers, BatteryConsumption, TransportedCargo, Damages, LocationLatitud, LocationLongitud) 
VALUES (CURRENT_TIMESTAMP,9, 13, 23, 33, 8, 25,25);

INSERT INTO OperatorsActivityLog(DateRegistration, OperatorUniqueID, Kilometers, BatteryConsumption, TransportedCargo, Damages, LocationLatitud, LocationLongitud) 
VALUES (CURRENT_TIMESTAMP,9, 14, 24, 34, 9, 26,26);
*/
