using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TPI
{
    internal class Barrack
    {
        static public int AutoIncrementalID { get; set; } = -1;
        public Location Location { get; set; } = Map.LandTypeLocation(EnumLandType.barrack).First();
        public List<Operator> Operators { get; set; } = new List<Operator>();
        
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
            foreach (Operator operatorx in Operators)
            {
                if(operatorx.GeneralStatus != EnumOperatorStatus.standBy)
                {
                    Console.Write(operatorx.ToString());
                    operatorx.MakeVoyage(Location);
                }
            }
        }

        /////////Accion 3
        public void TotalRecallFix()
        {
            foreach (Operator operatorx in Operators)
            {
                Console.Write(operatorx.ToString());
                if (operatorx.OperatorDamages.Count() > 0 && operatorx.GeneralStatus != EnumOperatorStatus.standBy)
                {
                    operatorx.MakeVoyage(Location);
                    operatorx.OperatorFix();
                }
                else
                    Console.WriteLine("No tiene daños que reparar");
            }
        }

        /////////Accion 4
        public void TotalBatteryRecharge()
        {
            foreach (Operator operatorx in Operators)
            {
                Console.Write(operatorx.ToString());
                if (Map.CheckLandType(operatorx.Location) == EnumLandType.barrack || Map.CheckLandType(operatorx.Location) == EnumLandType.dump && operatorx.GeneralStatus != EnumOperatorStatus.standBy)
                    operatorx.Battery.FullCapacity();
                else
                    Console.WriteLine("No està en una locaciòn donde pueda cargar la baterìa");
            }
        }

        /////////Accion 5
        public void OperatorSendLocation()
        {
            ShowOperators();

            bool operatorSelectionOK = OperatorEntryValidation(out int operatorID);
            bool destinationSelectionOK = Map.LocationEntryValidation(out Location destination);

            if (operatorSelectionOK && destinationSelectionOK)
                Operators[operatorID].MakeVoyage(destination);
        }

        /////////Accion 6
        public void OperatorLoad()
        {
            ShowOperators();

            bool operatorSelectionOK = OperatorEntryValidation(out int operatorID);
            bool loadOK = Operators[operatorID].Load.LoadEntryValidation(out int kilosToLoad);

            if (operatorSelectionOK && loadOK)
            {
                Operators[operatorID].OperatorLoad(kilosToLoad);
            }
        }


        /////////Accion 7
        public void OperatorReturnToBarrack()
        {
            ShowOperators();

            if (OperatorEntryValidation(out int operatorID))
            {
                Operators[operatorID].MakeVoyage(Location);
                Console.WriteLine($"El operador {Operators[operatorID]}a retornado al cuartel\n");
            }
        }


        /////////Accion 8
        public void AddOperator()
        {
            if (CommonFunctions.UserInputYesOrNo("Si desea crearlo automáticamente presione s"))
                CreateAnOperatorRandomly(1);
            else
            {
                Array operatorType = Enum.GetValues(typeof(EnumOperatorTypes));
                CommonFunctions.PrintList(operatorType.ToString().ToList());
                if (Enum.TryParse(CommonFunctions.UserInput("Ingrese el tipo del operador que desea crear"), true, out EnumOperatorTypes userOperatorTypeSeleccion))
                {
                    CreateAnOperatorSpecifically(userOperatorTypeSeleccion);
                }
                else
                {
                    Console.WriteLine("Por errores en la especificación del typo de operador se crea uno automáticamente");
                    CreateAnOperatorRandomly(1);
                }

            }
        }               

        /////////Accion 9
        public void RemoveOperator()
        {
            ShowOperators();

            if (OperatorEntryValidation(out int operatorID))
            {
                Console.WriteLine($"Has eliminado el operador: {Operators[operatorID]}\n");
                Operators.RemoveAt(operatorID);
            }
        }

        /////////Acciones 10 y 11
        public void SetOperatorStatus(EnumOperatorStatus status)
        {
            ShowOperators();

            if (OperatorEntryValidation(out int operatorID))
            {
                Console.WriteLine($"Has colocado al operador: {Operators[operatorID]} en status {status}\n");
                Operators[operatorID].SetStatus(status);
            }
        }

        /////////// Accion 12
        public void OperatorBatteryTransfer()
        {
            ShowOperators();
            Console.Write("Emisor. ");
            bool operatorIDFromSelectionOK = OperatorEntryValidation(out int operatorIDFrom);
            Console.Write("Receptor. ");
            bool operatorIDToSelectionOK = OperatorEntryValidation(out int operatorIDTo);

            if (operatorIDFromSelectionOK && operatorIDToSelectionOK)
                    Operators[operatorIDFrom].ElementTransferTo(Operators[operatorIDTo],Operators[operatorIDFrom].Battery, Operators[operatorIDTo].Battery);
        }
        /////////// Accion 13
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

        //////////////GENERALES
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

        public void CreateAnOperatorSpecifically(EnumOperatorTypes operatorx)
        {
                if (operatorx == EnumOperatorTypes.UAV)
                    Operators.Add(new OperatorUAV());
                if (operatorx == EnumOperatorTypes.K9)
                    Operators.Add(new OperatorK9());
                if (operatorx == EnumOperatorTypes.M8)
                    Operators.Add(new OperatorM8());
            Console.WriteLine($"Has creado un operador {operatorx} nuevo\n");
        }

        private bool OperatorEntryValidation(out int operatorID)
        {
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
    }
}
