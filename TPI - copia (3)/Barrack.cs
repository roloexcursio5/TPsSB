using System.Collections.Generic;

namespace TPI
{
    internal class Barrack
    {
        static public int AutoIncrementalID { get; set; } = -1;
        public Location Location { get; set; } = Map.LandTypeLocation(EnumLandType.barrack).First();
        public List<Operator> Operators { get; set; } = new List<Operator>();
        
        static public int UniqueID()
        {
            AutoIncrementalID++;
            return AutoIncrementalID;
        }
        public void CreateAnOperatorRandomly(int number)
        {
            Random random = new Random();

            for (int i = 0; i < number; i++)
            {
                int operatorType = random.Next(0, 3);
                if (operatorType == 0)
                    Operators.Add(new OperatorUAV());
                if (operatorType == 1)
                    Operators.Add(new OperatorK9());
                if (operatorType == 2)
                    Operators.Add(new OperatorM8());
            }
            Console.WriteLine($"Has creado {number} operador/es nuevo/s\n");
        }

        /////////Accion 0
        public void ShowOperators()
        {
            Console.WriteLine("\nOperadores:");
            CommonFunctions.PrintList(Operators);
        }

        /////////Accion 1
        public void ShowOperatorsInLocation()
        {
            if (Map.LocationEntryValidation(out Location location))
                ShowOperators(location);
        }

        private void ShowOperators(Location location)
        {
            IEnumerable<Operator> operatorsInLocation = Operators.Where(o => o.Location.Latitud == location.Latitud && o.Location.Longitud == location.Longitud);

            Console.WriteLine("\nOperadores:");
            if (operatorsInLocation.Count() > 0)
                CommonFunctions.PrintList(Operators.ToList());
            else
                Console.WriteLine("no hay operadores en esta localización");
        }


        /////////Accion 2
        public void TotalRecall()
        {
            //foreach (Operator operatorx in Operators)
            //    operatorx.ReturnToBarrack(this);
            //foreach (Operator operatorx in Operators)
            //{
            //    Console.Write(operatorx.ToString());
            //    operatorx.MakeVoyage(Location);
            //}


            //operatorx.ToString() + 
        }




        /////////Accion 3 enviar operador a una localización
        //FALTA
        public void OperatorSendLocation()
        {
            ShowOperators();
            //int operatorID = OperatorEntryValidation(CommonFunctions.UserInput("Seleccione el ID del operador:")); //return -1 for invalid user input

            //if (OperatorEntryValidation(out int operatorID) && Map.LocationEntryValidation(out Location destination))
                    //Operators[operatorID].MakeVoyage(destination);
                    //if (Operators[operatorID].TryVoyage(destination))
                    //{
                    //    Console.Write($"Viajaste de {Operators[operatorID].Location} a {destination}\n\n");
                    //    Console.WriteLine(Operators[operatorID].Battery.Actual);
                    //    Operators[operatorID].BatteryConsumption(destination);
                    //    Operators[operatorID].Location = destination;
                    //    Console.WriteLine(Operators[operatorID].Battery.Actual);

                    //}
            //if (location.Latitud != -1)
            //    ShowOperators(location);
        }

        /////////Accion 4
        public void OperatorLoad()
        {
            ShowOperators();
            //int operatorID = OperatorEntryValidation(CommonFunctions.UserInput("Seleccione el ID del operador:")); //return -1 for invalid user input

            //if (OperatorEntryValidation(out int operatorID))
            //{
            //    if(Operators[operatorID].LoadEntryValidation(out int kilosToLoad))
            //    {
            //        Operators[operatorID].OperatorLoad(kilosToLoad);
            //        //Operators[operatorID].Speed.ActualSpeedAdjustedByLoad(Operators[operatorID].Load);

            //    }
            //}
        }


        /////////Accion 5
        public void OperatorReturnToBarrack()
        {
            ShowOperators();
            //int operatorID = OperatorEntryValidation(CommonFunctions.UserInput("Seleccione el ID del operador:")); //return -1 for invalid user input

            //if (OperatorEntryValidation(out int operatorID))
            //{
            //    Operators[operatorID].MakeVoyage(Location);
            //    //Operators[operatorID].ReturnToBarrack(this);
            //     Console.WriteLine($"El operador {Operators[operatorID]}a retornado al cuartel\n");
            //}
        }

        private bool OperatorEntryValidation(out int operatorID)
        {

            //int operatorID = OperatorEntryValidation(); //return -1 for invalid user input
            string message = "";
            operatorID = -1;

            if (!int.TryParse(CommonFunctions.UserInput("Seleccione el ID del operador:"), out int operatorIDEntry))
                message += "Debe cargar un número";
            else
                operatorID = Operators.FindIndex(o => o.UniqueID == operatorIDEntry);

            if (operatorID == -1)
                message += "debe cargar un ID válido";

            CommonFunctions.IfErrorMessageShowIt(message);

            return message == "";
        }

        /* BORRAR
        private int OperatorEntryValidation(string userInput)
        {
            int i = -1;
            string message = "";

            if (int.TryParse(userInput, out int operatorID))
            {
                i = Operators.FindIndex(o => o.UniqueID == operatorID);
                message = i == -1 ? "debe cargar un ID válido" : "";
            }
            else
                message += "Debe cargar un nùmero";

            CommonFunctions.IfErrorMessageShowIt(message);

            return i;
        }
        */


        /////////Accion 7
        public void RemoveOperator()
        {
            ShowOperators();

            // BORRAR
            //string operatorIDInput = CommonFunctions.UserInput("Seleccione el ID del operador a eliminar o escriba NUEVO para crear uno aleatorio:");
            //int operatorID = -1;


            //if (operatorIDInput == "nuevo")
            //    CreateAnOperatorRandomly(1);
            //else
            //    operatorID = OperatorEntryValidation(operatorIDInput);   //return -1 for invalid user input

            if (OperatorEntryValidation(out int operatorID))
            {
                Console.WriteLine($"Has eliminado el operador: {Operators[operatorID]}\n");
                Operators.RemoveAt(operatorID);
            }
        }

        // BORRAR
        //public void AddOrRemoveOperator()
        //{
        //    ShowOperators();

        //    string operatorIDInput = CommonFunctions.UserInput("Seleccione el ID del operador a eliminar o escriba NUEVO para crear uno aleatorio:");
        //    int operatorID = -1;


        //    if (operatorIDInput == "nuevo")
        //        CreateAnOperatorRandomly(1);
        //    else
        //        operatorID = OperatorEntryValidation(operatorIDInput);   //return -1 for invalid user input

        //    if (operatorID != -1)
        //    {
        //        Console.WriteLine($"Has eliminado el operador: {Operators[operatorID].ToString()}\n");
        //        Operators.RemoveAt(operatorID);
        //    }
        //}

        /////////Accion 8 y 9
        public void SetOperatorStatus(EnumOperatorStatus status)
        {
            ShowOperators();

            if (OperatorEntryValidation(out int operatorID))
            {
                Console.WriteLine($"Has colocado al operador: {Operators[operatorID]} en status {status}\n");
                Operators[operatorID].SetStatus(status);
            }
        }

        /////////// TRANSFERENCIA
        public void OperatorBatteryTransfer()
        {
            ShowOperators();
            Console.Write("Emisor. ");
            bool operatorIDFromOK = OperatorEntryValidation(out int operatorIDFrom);
            Console.Write("Receptor. ");
            bool operatorIDToOK = OperatorEntryValidation(out int operatorIDTo);

            if (operatorIDFromOK && operatorIDToOK)
                    Operators[operatorIDFrom].ElementTransferTo(Operators[operatorIDTo],Operators[operatorIDFrom].Battery, Operators[operatorIDTo].Battery);
        }
        public void OperatorLoadTransfer()
        {
            ShowOperators();
            Console.Write("Emisor. ");
            bool operatorIDFromOK = OperatorEntryValidation(out int operatorIDFrom);
            Console.Write("Receptor. ");
            bool operatorIDToOK = OperatorEntryValidation(out int operatorIDTo);

            if (operatorIDFromOK && operatorIDToOK)
                Operators[operatorIDFrom].ElementTransferTo(Operators[operatorIDTo], Operators[operatorIDFrom].Load, Operators[operatorIDTo].Load);
        }

        /////////Accion 10
        //public void OperatorTransferBattery()
        //{
        //    ShowOperators();

        //    if (OperatorEntryValidation(out int operatorID))
        //        if (OperatorEntryValidation(out int operatorIDTheOther))
        //            Operators[operatorID].BatteryTransferTo(Operators[operatorIDTheOther]);
        //}


        /////////Accion 11
        public void OperatorTransferLoad()
        {
            ShowOperators();

            //if (OperatorEntryValidation(out int operatorID))
            //    if (OperatorEntryValidation(out int operatorIDTheOther))
            //        Operators[operatorID].LoadTransferTo(Operators[operatorIDTheOther]);
        }

        /////////Accion 12
        public void TotalRecallFix()
        {
            //foreach (Operator operatorx in Operators)
            //{
            //    Console.Write(operatorx.ToString());
            //    if (operatorx.OperatorDamages.Count() > 0)
            //    {
            //        operatorx.MakeVoyage(Location);
            //        operatorx.OperatorFix();
            //    }
            //    else
            //    {
            //        Console.WriteLine("No tiene daños que reparar");
            //    }
            //}
        }
        /////////Accion 12
        public void TotalBatteryRecharge()
        {
            //foreach (Operator operatorx in Operators)
            //{
            //    Console.Write(operatorx.ToString());
            //    //if (Map.CheckLandType(operatorx.Location) == EnumLandType.barrack || Map.CheckLandType(operatorx.Location) == EnumLandType.dump)
            //    //{
            //    //    operatorx.OperatorBatteryRecharge();
            //    //}
            //    //else
            //    //{
            //    //    Console.WriteLine("No està en una locaciòn donde pueda cargar la baterìa");
            //    //}
            //}
        }




        ///FALTA
        //public void Print()
        //{
        //    List<EnumOperatorTypes> operatorsTypes = Enum.GetValues(typeof(EnumOperatorTypes)).Cast<EnumOperatorTypes>().ToList();
        //    CommonFunctions.PrintList(operatorsTypes);

        //    if (operatorType == 0)
        //        Operators.Add(new OperatorUAV(UniqueID(), this));
        //    if (operatorType == 1)
        //        Operators.Add(new OperatorK9(UniqueID(), this));
        //    if (operatorType == 2)
        //        Operators.Add(new OperatorM8(UniqueID(), this));
        //    tipoString = Enum.TryParse<Tipo>(tipoString, true, out tipo) ? tipoString : null;
        //}
    }
}
