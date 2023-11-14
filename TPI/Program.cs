namespace TPI
{
    internal class Program
    {
        static public bool InProgram { get; set; } = true;
        static public bool InOperatorMenu { get; set; } = false;
        static public Map Map { get; set; }
        static public Barrack Barrack { get; set; }

        //static bool inProgram = true;
        //static bool inOperatorMenu = false;
        //static Map map;
        //static Barrack barrack;

        static void Main(string[] args)
        {
            Map = new Map();
            Barrack = new Barrack(Map);

            while (InProgram)
            {
                Barrack.ShowMainMenu();
                ApplyMainMenuSelection(Console.ReadLine().Trim().ToLower());

                while (InOperatorMenu)
                {
                    Barrack.selectedOperator.ShowOperatorMenu();
                    ApplyOperatorMenuSelection(Console.ReadLine().Trim().ToLower());
                }
            }
        }

        static void ApplyMainMenuSelection(string selectedOption)
        {
            switch (selectedOption)
            {
                case "1":
                    Barrack.ShowOperators();
                    break;
                case "2":
                    Barrack.ShowOperatorsInLocation(Map);
                    break;
                case "3":
                    Barrack.TotalRecall();
                    break;
                case "4":
                    InOperatorMenu = Barrack.IsAnOperatorSelected();
                    break;
                case "5":
                    Barrack.AddOrRemoveOperator();
                    break;
                case "s":
                    InProgram = false;
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
                    InOperatorMenu = false;
                    Console.WriteLine("\nHas salido del Menu operador al menu principal\n");
                    break;
                default:
                    Console.WriteLine("No has seleccionado ninguna opción válida, por lo tanto vuelves al menú de operador");
                    break;
            }
        }
    }
}

/*
1) Moverse una cantidad de kilómetros hacia otra localización, consumiendo
correspondientemente la batería. Nos mencionaron que por cada 10% utilizados de
carga, los operadores se mueven un 5% más lento
*/
/*
a) Enviarlo a una localización en especial.
b) indicar retorno a cuartel
c) cambiar estado a STANDBY - una entidad en STANDBY no puede ser
utilizada por comandos generales.
*/

/*
Vertedero: Sector lleno de basura, al pasar hay una chance de 5% de dañar
componentes del Operador. Debemos simular esto con un randomizador
Lago: Un sector inundado, las unidades K9 y M8 no pueden pasar.
- Vertedero electrónico: Un sector lleno de basura electrónica, no tiene chance
de causar daño físico al operador pero las ondas electromagnéticas de los
dispositivos dañan las baterías y reducen su capacidad máxima en un 20%
permanentemente.
- Cuartel: operadores pueden recargar batería o ser reparados.
- Sitio de reciclaje: Un sector dedicado a transformar basura en recursos útiles.
Estos sitios poseen puntos de recarga para los operadores pero no de
mantenimiento. Existen un máximo de 5 en todo el terreno.

Actualizar las rutinas de movimiento - no es necesario codear un movimiento
diagonal.
A) Generar rutinas para ordenar un operador moverse a una coordenada o localización en especial
B) Opcional: Programar una rutina que genere una ruta óptima (es decir, sin peligro) y otra que genere una ruta directa.
5) Nueva funcionalidad:
A) Orden general: Todos los operadores que no estén ocupados actualmente deben dirigirse al vertedero más cercano y recoger su cantidad máxima de carga para traer al sitio de reciclaje más cercano.
B) Orden general: Todos los operadores que estén dañados deben volver a un cuartel para mantenimiento.
C) Cambiar Batería: Reemplaza una batería dañada.
D) Simular daño: Un operador puede sufrir estos diferentes daños:
>MOTOR COMPROMETIDO: Reduce su velocidad promedio a la mitad.
>SERVO ATASCADO: No puede realizar operaciones de carga y descarga física
>BATERIA PERFORADA: Pierde batería un 500% más rápido en cada operación
>PUERTO BATERIA DESCONECTADO: No puede realizar operaciones de carga, recarga o transferencia de batería
>PINTURA RAYADA: No tiene efecto
*/



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