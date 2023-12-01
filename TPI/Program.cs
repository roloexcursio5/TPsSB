namespace TPI
{
    internal class Program
    {
        static public Barrack Barrack { get; set; }
        static public CommandMenu Menu { get; set; }

        static void Main(string[] args)
        {
            (Location[,], Barrack) MB = CommonFunctions.GetMatchStarted("Land","Barrack");
            Map.LatitudLongitud = MB.Item1;
            Barrack = MB.Item2;

            Menu = CommandMenu.GetInstance(Barrack);

            Menu.DisplayMenu();
        }
    }
}



////for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
////{
////    Console.Write("\n");
////    for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
////    {
////        Console.WriteLine(Map.LatitudLongitud[i, j].ToString());
////    }
////}
//CommonFunctions.SaveMatch(Barrack);


//for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
//{
//    Console.Write("\n");
//    for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
//    {
//        Console.WriteLine(Map.LatitudLongitud[i, j].ToString());
//    }
//}
// para imprimir el mapa
//for (int i = 0; i < map.latitudLongitud.GetLength(0); i++)
//{
//    Console.Write("\n");
//    for (int j = 0; j < map.latitudLongitud.GetLength(1); j++)
//    {
//        Console.Write(((Land)map.latitudLongitud[i, j]).type.ToString()[0] + "");
//    }

//}
