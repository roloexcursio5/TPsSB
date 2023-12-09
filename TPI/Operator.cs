using System.Security.Cryptography;

namespace TPI
{
    internal abstract class Operator
    {
        public int UniqueID { get; set; }
        public EnumOperatorStatus GeneralStatus { get; set; }
        public OperatorBattery Battery { get; set; } //mAh miliAmperios, 1000mAh = 1 hour use
        public OperatorLoad Load { get; set; } // kilos
        public int MaximumSpeed { get; set; } // kilometros/hora
        public Location Location { get; set; }
        public HashSet<EnumOperatorDamage> OperatorDamages { get; set; }

        public Operator()
        {
            SpeedDefinition(this);
        }
        private void SpeedDefinition(Operator o)
        {
            if (o is OperatorUAV)
                MaximumSpeed = 50;
            if (o is OperatorK9)
                MaximumSpeed = 40;
            if (o is OperatorM8)
                MaximumSpeed = 30;
        }

        /////////// TRANSFERENCIA
        protected bool TryTransferElementTo(Location operatorToLocation, OperatorElement operatorElementFrom,  OperatorElement operatorElementTo)
        {
            string message = "";

            if (!Map.CheckSameLocation(Location, operatorToLocation))
                message += "Los operadores no están en el mismo lugar\n";
            if (operatorElementFrom.ElementToGive() <= 0)
                message += Messages.NotEnoughElementToGiveMessage(operatorElementFrom);
            if (operatorElementTo.ElementToReceive() == 0)
                message += Messages.ElementToReceiveFullMessage(operatorElementFrom);

            if (message != "")
                message += Messages.ElementToTransferImposibilityMessage(operatorElementFrom);

            CommonFunctions.IfErrorMessageShowIt(message);

            return message == "";
        }

        protected int ElementTransferAmount(OperatorElement operatorElementFrom, OperatorElement operatorElemenTo)
        {
            int Give = operatorElementFrom.ElementToGive();
            int Receive = operatorElemenTo.ElementToReceive();

            return Give > Receive ? Receive : Give;
        }

        public void ElementTransferTo(Operator operatorTo, OperatorElement operatorElementFrom, OperatorElement operatorElementTo)
        {
            if (TryTransferElementTo(operatorTo.Location, operatorElementFrom, operatorElementTo))
            {
                int elementTransferedAmount = ElementTransferAmount(operatorElementFrom, operatorElementTo);

                operatorElementFrom.ElementTransfered(elementTransferedAmount);
                operatorElementTo.ElementReceived(elementTransferedAmount);

                Console.WriteLine($"Se transfirio {elementTransferedAmount} de {operatorElementFrom.ToString().Split(":")[0]} de\n{this}\na\n{operatorTo}");

                OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
                operatorTo.OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
            }
        }

        /// ACCION DE CARGA
        public void OperatorLoad(int load)
        {
            Load.ElementReceived (load);
            Console.WriteLine($"Cargaste al operador {UniqueID}con {load} kilos. Tu carga actual es de {Load.Actual} kilos, y tu carga máxima es de {Load.Maximum} kilos\n");
            OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
        }
        

        /// RECORRER DISTANCIA 
        public bool MakeVoyage(Location destination)
        {
            bool voyageSuccess = false; 

            if (TryVoyage(destination, out Stack<Land> trip))
            {
                Console.WriteLine($"Viajaste de {Location} a {destination}");
                CommonFunctions.PrintList(trip.Reverse().ToList());
                BatteryConsumption(destination, trip.Count());
                Location = destination;
                OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
                voyageSuccess = true;
            }

            return voyageSuccess;
        }
        private bool TryVoyage(Location destination, out Stack<Land> trip)
        {
            string message = "";

            trip = Map.Trip(this, destination, true);  // harcodeado el tema de que siempre sea el camino optimo FALTA trabajar para hacerlo optativo

            if (Map.CheckSameLocation(Location, destination))
                message += "\nYa está en destino\n";
            if (trip.Count() == 0)
                message += "Se intentó realizar el viaje pero no se pudo encontrar un camino para realizarlo por temas del terreno\n";
            if (trip.Count() > BatteryAutonomyDistance()) // un step es un kilometro
                message += "No te alcanza la bateria para llegar a destino\n\n";

            if(message != "")
                Console.WriteLine (message);

            return message == "";
        }
        private int BatteryAutonomyDistance()
        {
            return (int)((float)Battery.Actual / 1000 * ActualSpeed());
        }

        private int ActualSpeed()
        {
            return (int)(MaximumSpeed - 0.05f * (Load.LoadSpaceUsed() / 10));
        }

        private void BatteryConsumption(Location destination, int tripSteps)
        {
            int batteryConsumption = (int)((float)tripSteps / ActualSpeed() * 1000); // distance / speed = voyage hours
            Battery.Actual -= batteryConsumption;
            Console.WriteLine($"Consumiste {batteryConsumption} mAh de baterìa");
        }

        public void SetStatus(EnumOperatorStatus status)
        {
            GeneralStatus = status;
        }


        public override string ToString()
        {
            return $"ID: {UniqueID} - {GetType()} - {GeneralStatus} - {Location} - {Battery} - {Load} - Velocidad Máxima: {MaximumSpeed}";
        }

        public void OperatorFix()
        {
            if (TryFix())
            {
                string message = "";
                if (OperatorDamages.Contains(EnumOperatorDamage.compromisedEngine))
                    message += "Se ha reparado el motor\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.stuckServo))
                    message += "Se ha reparado el servo atascado\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.perforatedBattery))
                    message += "Se ha reparado la bateria perforada\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.disconnectedBatteryPort))
                    message += "Se ha reparado el puerto de la baterìa desconectado\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.scratchedPaint))
                    message += "Se ha vuelto a pintar el operador\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.reducedMaximumBatteryCapacity))
                    message += "Se ha incrementado el màximo de la baterìa a su estado original\n";

                Console.WriteLine(message);
                OperatorDamages.Clear();
            }
        }
        private bool TryFix()
        {
            string message = "";

            if (!(Map.CheckLandType(Location) == EnumLandType.barrack))
                message += "No estàs en el cuartel para poder realizar reparaciones\n";

            Console.WriteLine(message);

            return message == "";
        }

        public void OperatorBatteryRecharge()
        {
            if (TryBatteryRecharge())
            {
                Battery.BatteryRecharge();
            }
        }

        private bool TryBatteryRecharge()
        {
            string message = "";

            if (OperatorDamages.Contains(EnumOperatorDamage.perforatedBattery))
                message += "Se ha reparado la bateria perforada\n";
            if (OperatorDamages.Contains(EnumOperatorDamage.disconnectedBatteryPort))
                message += "Se ha reparado el puerto de la baterìa desconectado\n";

            Console.WriteLine(message);

            return message == "";
        }


        /////////////////////////////
        /// FALTA
        //public void BatteryFix()
        //{
        //    if (TryFix())
        //    {
        //        string message = "";
        //        if (OperatorDamages.Contains(EnumOperatorDamage.perforatedBattery))
        //            message += "Se ha reparado la bateria perforada\n";
        //        if (OperatorDamages.Contains(EnumOperatorDamage.disconnectedBatteryPort))
        //            message += "Se ha reparado el puerto de la baterìa desconectado\n";
        //        if (OperatorDamages.Contains(EnumOperatorDamage.reducedMaximumBatteryCapacity))
        //            message += "Se ha incrementado el màximo de la baterìa a su estado original\n";

        //        Console.WriteLine(message);
        //        OperatorDamages.Clear();
        //    }
        //}



        ////////Funciones para la carga del juego
        public void CompletoOperadorDesdeBaseSql(int OperatorUniqueID, EnumOperatorStatus OperatorGeneralStatus, int OperatorBattery, int OperatorLoad, int OperatorLocationLatitud, int OperatorLocationLongitud, EnumOperatorDamage OperatorDamages_)
        {
            UniqueID = OperatorUniqueID;
            GeneralStatus = OperatorGeneralStatus;
            Battery.Actual = OperatorBattery;
            Load.Actual = OperatorLoad;
            Location.Latitud = OperatorLocationLatitud;
            Location.Longitud = OperatorLocationLongitud;
            OperatorDamages.Add(OperatorDamages_);
        }

        static public Operator CargoOperadorDesdeBaseSQL(string tipo)
        {
            Operator operatorx = new OperatorUAV();

            if (tipo == "OperatorUAV")
            {
                operatorx = new OperatorUAV();
            }
            if (tipo == "OperatorK9")
            {
                operatorx = new OperatorK9();
            }
            if (tipo == "OperatorM8")
            {
                operatorx = new OperatorM8();
            }

            return operatorx;
        }
    }
}