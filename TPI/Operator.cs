namespace TPI
{
    internal abstract class Operator
    {
        public int uniqueID;
        public OperatorStatus generalStatus;
        public OperatorBattery battery;  //mAh miliAmperios, 1000mAh = 1 hour use
        public OperatorLoad load; // kilos
        public OperatorSpeed speed; // kilometros/hora
        public Location location; 
        //protected int maximumDistanceRange;

        public void ShowOperatorMenu()
        {
            Console.WriteLine(@"Seleccione una opción:
                                        1- Enviar operador a un destino
                                        2- Retornar operador al cuartel
                                        3- Cambiar estado del operador a ""STANDBY"" (no se le aplican los comandos generales)

                                        o presione s para salir");
        }


        /////////// TRANSFERENCIA DE BATERIA
         protected bool TryTransferBatteryFrom_To(Operator operatorTheOther)
        {
            string message = "";

            if (!location.CheckSameLocation(operatorTheOther))
                message += "Los operadores no están en el mismo lugar\n";
            if (battery.BatteryToGive() <= 0)
                message +=  "El operador se quedó con la reserva, no tiene batería que prestar\n";
            if (operatorTheOther.battery.BatteryToReceive() == 0) 
                message += "El operador receptor tiene la batería llena\n";

            if (message != "")
                Console.WriteLine(message);

            return message == "";
        }

        protected int BatteryTransferAmount(Operator operatorTheOther)
        {
            int Give = battery.BatteryToGive();
            int Receive = operatorTheOther.battery.BatteryToReceive();

            return Give > Receive ? Receive : Give;
        }

        public void BatteryTransferFrom_To(Operator operatorTheOther)
        {
            if (TryTransferBatteryFrom_To(operatorTheOther))
            {
                int batteryTransfered = BatteryTransferAmount(operatorTheOther);

                battery.actual -= batteryTransfered;
                operatorTheOther.battery.actual += batteryTransfered;

                Console.WriteLine($"Se transfirio {batteryTransfered} mAh de bateria de {this} a {operatorTheOther}");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la batería");
            }
        }


         /////////// TRANSFERENCIA DE CARGA
        protected bool TryTransferLoadFrom_To(Operator operatorTheOther)
        {
            string message = "";

            if (!location.CheckSameLocation(operatorTheOther))
                message += "Los operadores no están en el mismo lugar\n";
            if (load.LoadToGive() <= 0)
                message += "El operador no tiene carga que transferir\n";
            if (operatorTheOther.load.LoadToReceive() == 0)
                message += "El operador receptor tiene la capacidad de carga llena\n";

            if(message != "")
                Console.WriteLine(message);

            return message == "";
        }

        protected int LoadTransferAmount(Operator operatorTheOther)
        {
            int Give = load.LoadToGive();
            int Receive = operatorTheOther.load.LoadToReceive();

            return Give > Receive ? Receive : Give;
        }


        public void LoadTransferFrom_To(Operator operatorTheOther)
        {
            if (TryTransferLoadFrom_To(operatorTheOther))
            {
                int loadTransfered = LoadTransferAmount(operatorTheOther);

                load.actual -= loadTransfered;
                operatorTheOther.load.actual += loadTransfered;
                Console.WriteLine($"Se transfirio {loadTransfered} kilos de carga de {this} a {operatorTheOther}");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la carga");
            }
        }


        /////////////////////// ACCIONES DE VOLVER AL CUARTEL
        public void ReturnToBarrack(Barracks barrack)
        {
            location.latitud = barrack.location.latitud;
            location.longitud = barrack.location.latitud;
        }


        /// desde acá para abajo falta desarrollar mejor lo de locación y distancia
        /// RECORRER DISTANCIA
        /*
 
        destino
        */
        public void FullyUnload()
        {
            load.actual = 0;
        }

        protected void FullyBatteryCharge()
        {
            battery.actual = battery.maximum;
        }
        public int MaximumDistanceRangeOrBatteryAutonomy()
        {
            return battery.actual / 1000 * speed.actual;

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
            int voyageHours = distance / speed.actual;
            return o.battery.actual -= voyageHours * 1000;
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
                o.battery.actual -= batteryConsumption;
                Console.WriteLine($"Se consumió {batteryConsumption} mAh de bateria por el viaje realizado");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la batería");
            }
        }


    }
}

