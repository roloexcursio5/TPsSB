using System.Diagnostics.CodeAnalysis;

namespace TPI
{
    internal abstract class Operator
    {
        public int UniqueID { get; set; }
        public EnumOperatorStatus GeneralStatus { get; set; }
        public OperatorBattery Battery { get; set; } //mAh miliAmperios, 1000mAh = 1 hour use
        public OperatorLoad Load { get; set; } // kilos
        public OperatorSpeed Speed { get; set; } // kilometros/hora
        public Location Location { get; set; }
        public HashSet<EnumOperatorDamage> OperatorDamages { get; set; } = new HashSet<EnumOperatorDamage>();

        /////////// TRANSFERENCIA DE BATERIA
        protected bool TryTransferBatteryTo(Operator operatorTheOther)
        {
            string message = "";

            
            //if (!Location.CheckSameLocation(operatorTheOther))
            if (!Map.CheckSameLocation(Location, operatorTheOther.Location))
                //Map.CalculateDistance(Location, operatorTheOther.Location) != 0)
                message += "Los operadores no están en el mismo lugar\n";
            if (Battery.BatteryToGive() <= 0)
                message += "El operador se quedó con la reserva, no tiene batería que prestar\n";
            if (operatorTheOther.Battery.BatteryToReceive() == 0)
                message += "El operador receptor tiene la batería llena\n";

            if (message != "")
                message += "No se pudo transferir la batería\n";

            CommonFunctions.IfErrorMessageShowIt(message);

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

                OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
            }
        }


        /////////// TRANSFERENCIA DE CARGA
        protected bool TryTransferLoadTo(Operator operatorTheOther)
        {
            string message = "";

            //if (!Location.CheckSameLocation(operatorTheOther))
            if (!Map.CheckSameLocation(Location, operatorTheOther.Location))
                message += "Los operadores no están en el mismo lugar\n";
            if (Load.LoadToGive() <= 0)
                message += "El operador no tiene carga que transferir\n";
            if (operatorTheOther.Load.LoadToReceive() == 0)
                message += "El operador receptor tiene la capacidad de carga llena\n";

            if (message != "")
                message += "No se pudo transferir la carga\n";

            CommonFunctions.IfErrorMessageShowIt(message);

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

                OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
            }
        }


        /////////////////////// ACCIONES DE VOLVER AL CUARTEL
        //public void ReturnToBarrack(Barrack barrack)
        //{
        //    Location.Latitud = barrack.Location.Latitud;
        //    Location.Longitud = barrack.Location.Longitud;
        //}


        /// ACCION DE CARGA
        public bool LoadEntryValidation(out int load)
        {
            string message = "";
            load = 0;

            if (!int.TryParse(CommonFunctions.UserInput("Ingrese la cantidad de Kilos a cargar"), out load))
                message += "Debes cargar un número\n";
            if (load < 1)
                message += "Los kilos tienen que ser un número positivo mayor que cero\n";
            if (load > Load.LoadToReceive())
                message += $"Tu peso actual es de {Load.Actual} kilo y quieres cargar {load} kilos por lo cual excedes la capacidad máxima que es de {Load.Maximum} kilos\n";

            CommonFunctions.IfErrorMessageShowIt(message);

            //if (message != "")
            //    load = -1;

            return message == "";
        }

        public void OperatorLoad(int load)
        {
            Load.Actual += load;
            Console.WriteLine($"Cargaste al operador {UniqueID}con {load} kilos. Tu carga actual es de {Load.Actual} kilos, y tu carga máxima es de {Load.Maximum} kilos\n");
            OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
        }
        

        /// RECORRER DISTANCIA 
        /// desde acá para abajo falta desarrollar mejor lo de locación y distancia
        /*
            ingresa destino (esto debería estar)
            calcula distancia (mapa)
            calcula peso (cuando carga ajusta peso) --> calcula velocidad
            calcula distancia según batería // autonomía batería en batería
            chequea distancia destino vs distancia batería
        realiza viaje ajusta la localización del operador
        descuenta bateria // en batería
        */

 
        public void MakeVoyage(Location destination)
        {
            if (TryVoyage(destination))
            {
                Console.Write($"Viajaste de {Location} a {destination}\n\n");
                Map.JourneyStepsPrint(this, destination, true); // harcodeado trabajar
                BatteryConsumption(destination);
                Location = destination;
                OperatorDamages.UnionWith(new Damages().DamageRandomCreator());
            }
        }
        private bool TryVoyage(Location destination)
        {

            //int distanceToDestination = -1;
            //Location destinationLocation = Map.CheckValidLocation(); //return location.Latitud -1 for invalid Location

            //if (destinationLocation.Latitud != -1)
            //    distanceToDestination = Map.CalculateDistance(Location, destinationLocation);
            //if (distanceToDestination == 0)
            //    message += "Ya estás en destino\n";
            //if (distanceToDestination > Battery.DistanceBatteryAutonomy(Speed))
            //    message += "No te alcanza la bateria para llegar a destino\n";

            //CommonFunctions.IfErrorMessageShowIt(message);

            //if (message != "")
            //    distanceToDestination = -1;

            //return distanceToDestination;

            //if (Map.CheckValidLocation(out Location destination) &&
            
            string message = "";
            if (Map.CheckSameLocation(Location, destination))
                message += "Ya está en destino\n";
            if (Map.JourneySteps(this, destination, true).Count() == 0)
                message += "No es posible realizar el viaje por temas del terreno\n";
            if (Map.JourneySteps(this, destination, true).Count() > BatteryAutonomyDistance()) // un step es un kilometro
                message += "No te alcanza la bateria para llegar a destino\n\n";
                
            if(message != "") Console.WriteLine(message);

            return message == "";
        }
        private int BatteryAutonomyDistance()
        {
            return Battery.Actual / 1000 * ActualSpeed();
        }

        private int ActualSpeed()
        {
            return (int)(Speed.Maximum - 0.05f * (LoadSpaceUsed() / 10));
        }

        private int LoadSpaceUsed()
        {
            return Load.Actual / Load.Maximum * 100;
        }

        private void BatteryConsumption(Location destination)
        {
            Battery.Actual -= (int)((float)Map.JourneySteps(this, destination, true).Count() / ActualSpeed() * 1000); // distance / speed = voyage hours
        }

        public void SetStatus(EnumOperatorStatus status)
        {
            GeneralStatus = status;
        }


        public override string ToString()
        {
            return $"ID: {UniqueID} - {GetType()} - {GeneralStatus} - {Location} - {Battery} - {Load}\n";
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


        /// <summary>
        /// FAKAT
        /// </summary>
        public void BatteryFix()
        {
            if (TryFix())
            {
                string message = "";
                if (OperatorDamages.Contains(EnumOperatorDamage.perforatedBattery))
                    message += "Se ha reparado la bateria perforada\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.disconnectedBatteryPort))
                    message += "Se ha reparado el puerto de la baterìa desconectado\n";
                if (OperatorDamages.Contains(EnumOperatorDamage.reducedMaximumBatteryCapacity))
                    message += "Se ha incrementado el màximo de la baterìa a su estado original\n";

                Console.WriteLine(message);
                OperatorDamages.Clear();
            }
        }
    }
}
//public void FullyUnload()
//{
//    Load.Actual = 0;
//}


//protected void FullyBatteryCharge()
//{
//    Battery.Actual = Battery.Maximum;
//}