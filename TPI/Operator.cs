namespace TPI
{
    internal abstract class Operator
    {
        //private int uniqueID;
        //protected OperatorStatus generalStatus;
        //protected OperatorBattery battery;  //mAh miliAmperios, 1000mAh = 1 hour use
        //protected OperatorLoad load; // kilos
        //protected OperatorSpeed speed; // kilometros/hora
        //protected Location location;

        public int UniqueID { get; set; }
        public OperatorStatus GeneralStatus { get; set; }
        public OperatorBattery Battery { get; set; } //mAh miliAmperios, 1000mAh = 1 hour use
        public OperatorLoad Load { get; set; } // kilos
        public OperatorSpeed Speed { get; set; } // kilometros/hora
        public Location Location { get; set; }

        public void ShowOperatorMenu()
        {
            Console.WriteLine(@"Seleccione una opción:
                                        1- Enviar operador a un destino
                                        2- Retornar operador al cuartel
                                        3- Cambiar estado del operador a ""STANDBY"" (no se le aplican los comandos generales)

                                        o presione s para salir");
        }


        /////////// TRANSFERENCIA DE BATERIA
         protected bool TryTransferBatteryTo(Operator operatorTheOther)
        {
            string message = "";

            if (!Location.CheckSameLocation(operatorTheOther))
                message += "Los operadores no están en el mismo lugar\n";
            if (Battery.BatteryToGive() <= 0)
                message +=  "El operador se quedó con la reserva, no tiene batería que prestar\n";
            if (operatorTheOther.Battery.BatteryToReceive() == 0) 
                message += "El operador receptor tiene la batería llena\n";

            if (message != "")
                Console.WriteLine(message);

            return message == "";
        }

        protected int BatteryTransferAmount(Operator operatorTheOther)
        {
            int Give = Battery.BatteryToGive();
            int Receive = operatorTheOther.Battery.BatteryToReceive();

            return Give > Receive ? Receive : Give;
        }

        public void BatteryTransferTo(Operator operatorTheOther)
        {
            if (TryTransferBatteryTo(operatorTheOther))
            {
                int batteryTransfered = BatteryTransferAmount(operatorTheOther);

                Battery.Actual -= batteryTransfered;
                operatorTheOther.Battery.Actual += batteryTransfered;

                Console.WriteLine($"Se transfirio {batteryTransfered} mAh de bateria de {this} a {operatorTheOther}");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la batería");
            }
        }


         /////////// TRANSFERENCIA DE CARGA
        protected bool TryTransferLoadTo(Operator operatorTheOther)
        {
            string message = "";

            if (!Location.CheckSameLocation(operatorTheOther))
                message += "Los operadores no están en el mismo lugar\n";
            if (Load.LoadToGive() <= 0)
                message += "El operador no tiene carga que transferir\n";
            if (operatorTheOther.Load.LoadToReceive() == 0)
                message += "El operador receptor tiene la capacidad de carga llena\n";

            if(message != "")
                Console.WriteLine(message);

            return message == "";
        }

        protected int LoadTransferAmount(Operator operatorTheOther)
        {
            int Give = Load.LoadToGive();
            int Receive = operatorTheOther.Load.LoadToReceive();

            return Give > Receive ? Receive : Give;
        }


        public void LoadTransferTo(Operator operatorTheOther)
        {
            if (TryTransferLoadTo(operatorTheOther))
            {
                int loadTransfered = LoadTransferAmount(operatorTheOther);

                Load.Actual -= loadTransfered;
                operatorTheOther.Load.Actual += loadTransfered;
                Console.WriteLine($"Se transfirio {loadTransfered} kilos de carga de {this} a {operatorTheOther}");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la carga");
            }
        }


        /////////////////////// ACCIONES DE VOLVER AL CUARTEL
        public void ReturnToBarrack(Barrack barrack)
        {
            Location.Latitud = barrack.Location.Latitud;
            Location.Longitud = barrack.Location.Latitud;
        }


        /// desde acá para abajo falta desarrollar mejor lo de locación y distancia
        /// RECORRER DISTANCIA
        /*
 
        destino
        */
        public void FullyUnload()
        {
            Load.Actual = 0;
        }

        protected void FullyBatteryCharge()
        {
            Battery.Actual = Battery.Maximum;
        }
        public int MaximumDistanceRangeOrBatteryAutonomy()
        {
            return Battery.Actual / 1000 * Speed.Actual;
        }

        protected int DistanceCalculationToDestination(Operator o)
        {
            // A COMPLETAR
            int locationDeparture = 0;
            int locationArrival = 0;
            return locationArrival - locationDeparture;
        }

        protected bool CheckIfVoyageCanBeMade(Operator o)
        {
            int batteryAutonomy = MaximumDistanceRangeOrBatteryAutonomy();
            int distance = DistanceCalculationToDestination(o);

            return batteryAutonomy > distance;
        }

        private int  BatteryConsumption(Operator o)
        {
            int distance = DistanceCalculationToDestination(o);
            int voyageHours = distance / Speed.Actual;
            return o.Battery.Actual -= voyageHours * 1000;
        }

        protected bool TryVoyage(Operator o)
        {
            string message = "";

            if (!CheckIfVoyageCanBeMade(o))
                message += "No tiene suficiente bateria para llegar a destino\n";

            Console.WriteLine(message);

            return message == "";
        }

        public void MakeVoyageTo(Operator o)
        {
            if (TryVoyage(o))
            {
                int batteryConsumption = BatteryConsumption(o);
                o.Battery.Actual -= batteryConsumption;
                Console.WriteLine($"Se consumió {batteryConsumption} mAh de bateria por el viaje realizado");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la batería");
            }
        }


    }
}

//protected int maximumDistanceRange;