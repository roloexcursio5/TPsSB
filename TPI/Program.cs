namespace TPI
{
    internal class Program
    {
        static bool exitProgram = false;
        static bool exitOperatorMenu = true;
        static string selectedOption = "";
        static Barracks barrack = new Barracks();

        static void Main(string[] args)
        {
            while (!exitProgram)
            {
                ShowMainMen();
                selectedOption = UsersMenuSelection();
                ApplyMainMenuSelection(selectedOption);

                while (!exitOperatorMenu)
                {
                    ShowOperatorMenu();
                    selectedOption = UsersMenuSelection();
                    ApplyOperatorMenuSelection(selectedOption);
                }
            }
            
 
        }
        static void ShowMainMen()
        {
            Console.WriteLine(@"Seleccione una opción:
                        1- Listar el estado de todos los operadores
                        2- Listar el estado de todos los operadores en un lugar
                        3- Llamado y retorno de todos los operadores (Total recall)
                        4- Seleccionar un operador específico para realizar una acción)
                        5- Agregar o remover operadores de la Reserva

                        o presione s para salir");
        }

        static string UsersMenuSelection()
        {
            Console.Write("Su eleccion: ");
            string input = Console.ReadLine().Trim().ToLower();
            return input;
        }
       

        static void ApplyMainMenuSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "1":
                    barrack.ShowOperatos();
                    break;
                case "2":
                    //
                    break;
                case "3":
                    //
                    break;
                case "4":
                    // exitOperatorMenu = false;
                    break;
                case "5":
                    //
                    break;
                case "s":
                    exitProgram = true;
                    Console.WriteLine("Has salido del programa");
                    break;
                default:
                    Console.WriteLine("No has seleccionado ninguna opción válida, por lo tanto vuelves al menú principal");
                    break;
            }
        }

        static void ShowOperatorMenu()
        {
            Console.WriteLine(@"Seleccione una opción:
                                        1- Enviar operador a un destino
                                        2- Retornar operador al cuartel
                                        3- Cambiar estado del operador a ""STANDBY"" (no se le aplican los comandos generales)

                                        o presione s para salir");
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
                    exitOperatorMenu = true;
                    Console.WriteLine("\nHas salido del Menu operador al menu principal\n");
                    break;
                default:
                    Console.WriteLine("No has seleccionado ninguna opción válida, por lo tanto vuelves al menú de operador");
                    break;
            }
        }
    }
}