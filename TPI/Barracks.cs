namespace TPI
{
    internal class Barracks
    {
        private int IDautoincremental = 0;
        public Location location;
        public List<Operator> operators = new List<Operator>();
        public Operator selectedOperator;

        public Barracks(Map map)
        {
            FillBarrackLocation(map);
            CreateAnOperatorRandomly(10);
        }

        private void FillBarrackLocation(Map map)
        {
            for (int i = 0; i < map.latitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < map.latitudLongitud.GetLength(1); j++)
                {
                    if (((Land)map.latitudLongitud[i, j]).type == LandType.barracks)
                    {
                        location = new Location(i, j);
                    }
                }
            }
        }

        private int UniqueID()
        {
            IDautoincremental++;
            return IDautoincremental;
        }

        private void CreateAnOperatorRandomly(int number)
        {
            Random random = new Random();

            for (int i = 0; i < number; i++)
            {
                int operatorType = random.Next(3);
                if (operatorType == 0)
                    operators.Add(new OperatorUAV(UniqueID(), this));
                if (operatorType == 1)
                    operators.Add(new OperatorK9(UniqueID(), this));
                if (operatorType == 2)
                    operators.Add(new OperatorM8(UniqueID(), this));
            }
        }


        public void ShowMainMenu()
        {
            Console.WriteLine(@"Seleccione una opción:
                        1- Listar el estado de todos los operadores
                        2- Listar el estado de todos los operadores en un lugar
                        3- Llamado y retorno de todos los operadores (Total recall)
                        4- Seleccionar un operador específico para realizar una acción)
                        5- Agregar o remover operadores de la Reserva

                        o presione s para salir

Su eleccion");
        }

        /////////Accion 1
        public void ShowOperatos()
        {
            Console.WriteLine("\nOperadores:");
            for (int i = 0; i < operators.Count; i++)
            {
                Console.WriteLine($"{operators[i].uniqueID} - {operators[i]} - {operators[i].generalStatus} - {operators[i].location.ToString()}");
            }
            Console.WriteLine("\n\n");
        }

        /////////Accion 2
        public void ShowOperatosInLocation(Map map)
        {
            Console.WriteLine("Indique latitud:\t");
            string latitudInput = Console.ReadLine().Trim().ToLower();
            Console.WriteLine("Indique longitud:\t");
            string longitudInput = Console.ReadLine().Trim().ToLower();
            
            if (map.CheckMapValidLocation(latitudInput, longitudInput))
            {
                Location location = new Location(int.Parse(latitudInput), int.Parse(longitudInput));
                ShowOperatos(location);
            }
            else
            {
                Console.WriteLine("Por errores en el ingreso de datos vuelves al menú principal\n\n");
            }
        }

        private void ShowOperatos(Location location)
        {
            string message = "";
            
            foreach(Operator operatorx in operators)
            {
                if (operatorx.location.latitud == location.latitud && operatorx.location.longitud == location.longitud)
                    message += ($"{operatorx.uniqueID} - {operatorx} - {operatorx.generalStatus}\n");
            }

            if(message != "")
            {
                Console.WriteLine("\nOperadores:\n" + message);
            }
            else
            {
                Console.WriteLine("no hay operadores en esta localizaciòn\n\n");
            }
        }

        /////////Accion 3
        public void TotalRecall()
        {
            foreach(Operator operatorx in this.operators)
            {
                operatorx.ReturnToBarrack(this);
            }

            Console.WriteLine($"Todos los operadores han retornado a la base localizada en: {location.ToString()}\n");
        }

        /////////Accion 4
        public bool IsAnOperatorSelected()
        {
            ShowOperatos();

            Console.WriteLine("Seleccione el ID del operador:\t");
            string operatorIDInput = Console.ReadLine().Trim().ToLower();
            bool validSelection = CheckValidOperatorIDSelection(operatorIDInput);

            if (validSelection)
                selectedOperator = operators[int.Parse(operatorIDInput)];

            return validSelection;
        }

        private bool CheckValidOperatorIDSelection(string input)
        {
            int i = -1;
            string message = "";

            if (int.TryParse(input, out int operatorID))
            {
                i = operators.FindIndex(o => o.uniqueID == operatorID);
                message = i == -1 ? "debe cargar un ID válido" : "";
            }
            else
                message += "debe cargar un nùmero";

            if (message != "")
                Console.WriteLine(message + "\nPor errores en el ingreso de datos vuelves al menú principal\n\n");

            return i != -1;
        }

        /////////Accion 5
        public void AddOrRemoveOperator()
        {
            ShowOperatos();

            Console.WriteLine("Seleccione el ID del operador a eliminar o escriba NUEVO para crear uno aleatorio:\t");
            string operatorIDInput = Console.ReadLine().Trim().ToLower();

            if(operatorIDInput == "nuevo")
            {
                CreateAnOperatorRandomly(1);
                Console.WriteLine("Has creado un nuevo operador");
                ShowOperatos();
            }
            else if(CheckValidOperatorIDSelection(operatorIDInput))
            {
                int i = operators.FindIndex(o => o.uniqueID == int.Parse(operatorIDInput));
                operators.RemoveAt(i);
                Console.WriteLine($"Has eliminado el operador con ID: {i}");
                ShowOperatos();
            }
        }
    }
}
