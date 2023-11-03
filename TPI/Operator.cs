namespace TPI
{
    internal abstract class Operator
    {
        protected string uniqueID;
        public OperatorBattery battery;  //mAh miliAmperios, 1000mAh = 1 hour use
        public OperatorStatus generalStatus;
        public OperatorLoad load; // kilos
        protected int speedAdjustedByLoad;
        protected int optimalSpeed; // kilometros/hora
        protected int maximumDistanceRange;
        protected string location; // actual

        protected string UniqueID()
        {
            Guid uniqueID = Guid.NewGuid();
            return uniqueID.ToString();
        }


        protected int LoadPercentage()
        {
            return load.actual / load.maximum * 100;
        }

        protected int SpeedAdjustedByLoad()
        {
            return (int) (optimalSpeed * 0.05f * (LoadPercentage()/10));

        }

        public int MaximumDistanceRangeOrBatteryAutonomy()
        {
            return battery.actual/1000 * SpeedAdjustedByLoad();
            
        }

        protected string CheckIfOperatorsAreInSameLocation(Operator o1, Operator o2)
        {
            string message = (o1.location != o2.location) ? "Los operadores no están en el mismo lugar\n" : "";

            return message;
        }

        /////////// TRANSFERENCIA DE BATERIA
         protected bool TryTransferBattery(Operator oFrom, Operator oTo)
        {
            string message = "";

            // si tiene error te va tirando el error que tiene y al final te arroja falso
            message += CheckIfOperatorsAreInSameLocation(oFrom, oTo);
            message += oFrom.battery.CheckBatteryToGive();
            message += oTo.battery.CheckBatteryToReceive();

            Console.WriteLine(message);

            return message == "";
        }

        protected int BatteryTransferAmount(Operator oFrom, Operator oTo)
        {
            int Give = oFrom.battery.BatteryToGive();
            int Receive = oTo.battery.BatteryToReceive();

            return Give > Receive ? Receive : Give;
        }

        public void BatteryTransferFrom_To(Operator oFrom, Operator oTo)
        {
            if (TryTransferBattery(oFrom, oTo))
            {
                int batteryTransfered = BatteryTransferAmount(oFrom, oTo);

                oFrom.battery.actual -= batteryTransfered;
                oTo.battery.actual += batteryTransfered;

                Console.WriteLine($"Se transfirio {batteryTransfered} mAh de bateria de {oFrom} a {oTo}");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la batería");
            }
        }


        /////////// TRANSFERENCIA DE CARGA
        protected bool TryTransferLoad(Operator oFrom, Operator oTo)
        {
            string message = "";

            // si tiene error te va tirando el error que tiene y al final te arroja falso
            message += CheckIfOperatorsAreInSameLocation(oFrom, oTo);
            message += oFrom.load.CheckLoadToGive();
            message += oTo.load.CheckLoadToReceive();

            Console.WriteLine(message);

            return message == "";
        }

        protected int LoadTransferAmount(Operator oFrom, Operator oTo)
        {
            int Give = oFrom.load.LoadToGive();
            int Receive = oTo.load.LoadToReceive();

            return Give > Receive ? Receive : Give;
        }


        public void LoadTransferFrom_To(Operator oFrom, Operator oTo)
        {
            if (TryTransferLoad(oFrom, oTo))
            {
                int loadTransfered = LoadTransferAmount(oFrom, oTo);

                oFrom.load.actual -= loadTransfered;
                oTo.load.actual += loadTransfered;
                Console.WriteLine($"Se transfirio {loadTransfered} kilos de carga de {oFrom} a {oTo}");
            }
            else
            {
                Console.WriteLine("No se pudo transferir la carga");
            }
        }




        /// desde acá para abajo falta desarrollar mejor lo de locación y distancia
        /// RECORRER DISTANCIA
        /*
 
        destino
        */
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
            int voyageHours = distance / SpeedAdjustedByLoad();
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

        protected void FullyUnload()
        {
            load.actual = 0;
        }

        protected void FullyBatteryCharge()
        {
            battery.actual = battery.maximum;
        }
    }
}

