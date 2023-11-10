namespace TPI
{
    internal class Program
    {
        static bool inProgram = true;
        static bool inOperatorMenu = false;
        static Map map;
        static Barracks barrack;

        static void Main(string[] args)
        {
            map = new Map();
            barrack = new Barracks(map);

            while (inProgram)
            {
                barrack.ShowMainMenu();
                ApplyMainMenuSelection(Console.ReadLine().Trim().ToLower());

                while (inOperatorMenu)
                {
                    barrack.selectedOperator.ShowOperatorMenu();
                    ApplyOperatorMenuSelection(Console.ReadLine().Trim().ToLower());
                }
            }
        }

        static void ApplyMainMenuSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "1":
                    barrack.ShowOperatos();
                    break;
                case "2":
                    barrack.ShowOperatosInLocation(map);
                    break;
                case "3":
                    barrack.TotalRecall();
                    break;
                case "4":
                    inOperatorMenu = barrack.IsAnOperatorSelected();
                    break;
                case "5":
                    barrack.AddOrRemoveOperator();
                    break;
                case "s":
                    inProgram = false;
                    Console.WriteLine("Has salido del programa");
                    break;
                default:
                    Console.WriteLine("No has seleccionado ninguna opción válida, por lo tanto vuelves al menú principal");
                    break;
            }
        }

        static void ApplyOperatorMenuSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "1":
                    //
                    break;
                case "2":
                    //
                    break;
                case "3":
                    //
                    break;
                case "s":
                    inOperatorMenu = false;
                    Console.WriteLine("\nHas salido del Menu operador al menu principal\n");
                    break;
                default:
                    Console.WriteLine("No has seleccionado ninguna opción válida, por lo tanto vuelves al menú de operador");
                    break;
            }
        }
    }
}





// para imprimir el mapa
//for (int i = 0; i < map.latitudLongitud.GetLength(0); i++)
//{
//    Console.Write("\n");
//    for (int j = 0; j < map.latitudLongitud.GetLength(1); j++)
//    {
//        Console.Write(((Land)map.latitudLongitud[i, j]).type.ToString()[0] + "");
//    }

//}

// para encontrar el cuartel
//for (int i = 0; i < map.latitudLongitud.GetLength(0); i++)
//{
//    for (int j = 0; j < map.latitudLongitud.GetLength(1); j++)
//    {
//        if(((Land)map.latitudLongitud[i, j]).type == LandType.barracks)
//            Console.Write("\n" + i + "-" + j + "\n");
//    }

//}

//Console.WriteLine(barrack.location.latitud + "-" + barrack.location.longitud);


//OperatorUAV operatorUAV = new OperatorUAV(1, barrack);
//OperatorM8 operatorM8 = new OperatorM8(2, barrack);

//Console.WriteLine(operatorUAV.battery.actual);
//Console.WriteLine(operatorM8.battery.actual);
//operatorUAV.BatteryTransferFrom_To(operatorM8);
//Console.WriteLine(operatorUAV.battery.actual);
//Console.WriteLine(operatorM8.battery.actual);


//Console.WriteLine(operatorUAV.load.actual);
//Console.WriteLine(operatorM8.load.actual);
//operatorUAV.LoadTransferFrom_To(operatorM8);
//Console.WriteLine(operatorUAV.load.actual);
//Console.WriteLine(operatorM8.load.actual);
//static string UsersInputSelection(string message)
//{
//    Console.Write(message);
//    string input = Console.ReadLine().Trim().ToLower();
//    return input;
//}