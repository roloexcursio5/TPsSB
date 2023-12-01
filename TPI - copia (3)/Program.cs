namespace TPI
{
    internal class Program
    {
        static public Barrack Barrack { get; set; }
        static public CommandMenu Menu { get; set; }
        //borrar static public List<Operator> Operators { get; set; }
        static void Main(string[] args)
        {
            //Map.CreateMapRandomly();
            //Barrack = new Barrack();

            ////for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
            ////{
            ////    Console.Write("\n");
            ////    for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
            ////    {
            ////        Console.WriteLine(Map.LatitudLongitud[i, j].ToString());
            ////    }
            ////}
            //CommonFunctions.SaveMatch(Barrack);




            (Location[,], Barrack) MB = CommonFunctions.GetMatchStarted("Land","Barrack");
            Map.LatitudLongitud = MB.Item1;
            Barrack = MB.Item2;



            //for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
            //{
            //    Console.Write("\n");
            //    for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
            //    {
            //        Console.WriteLine(Map.LatitudLongitud[i, j].ToString());
            //    }
            //}

            CommonFunctions.PrintList(Barrack.Operators);
            Console.WriteLine(Barrack.Location);
            Console.WriteLine(Barrack.AutoIncrementalID);

            CommonFunctions.SaveMatch(Barrack);



            Menu = CommandMenu.GetInstance(Barrack);

            Menu.DisplayMenu();





            /*
            Console.WriteLine(Map.LandTypeLocation(EnumLandType.recyclingSite).Count());
            foreach(Location l in Map.LandTypeLocation(EnumLandType.recyclingSite))
                Console.WriteLine(l);
            Console.WriteLine(Barrack.Location);

            Location closest = Map.ClosestLandTypeLocation(Barrack.Operators[0], EnumLandType.recyclingSite);
            Console.WriteLine(closest);

            Stack<Land> journey = Map.JourneySteps(Barrack.Operators[0], closest, true);
            Console.WriteLine(journey.Count());

            foreach (Land land in journey.Reverse())
                Console.WriteLine(land);

            Console.WriteLine("paso por aca");

            //foreach (Land step in journey)
            //    Console.WriteLine(step);
            //


            for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
            {
                Console.Write("\n");
                for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
                {
                    Console.Write(((Land)Map.LatitudLongitud[i, j]).LandType.ToString()[0]);
                }

            }

            */

            //OperatorUAV uav = new OperatorUAV(0, Barrack);
            //OperatorM8 m8 = new OperatorM8(0, Barrack);
            //OperatorK9 k9 = new OperatorK9(0, Barrack);
            //Location origen = new Location(5,5);
            //Location destino = new Location(10,10);
            //Map.SubMapComplete(origen, destino, k9);
            //Map.JourneySteps();

            //Menu = CommandMenu.GetInstance(Barrack);

            //Menu.DisplayMenu();
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