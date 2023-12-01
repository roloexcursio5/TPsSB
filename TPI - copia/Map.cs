namespace TPI
{
    internal static class Map
    {
        static public Location[,] LatitudLongitud { get; set; } = new Location[100, 100];

        static public void CreateMapRandomly()
        {
            int barracksCount = 1;
            int recyclingSiteCount = 3;
            int electronicDumpCount = 100;

            for (int i = 0; i < LatitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < LatitudLongitud.GetLength(1); j++)
                {
                    LatitudLongitud[i, j] = new Land(i, j);
                    ((Land)LatitudLongitud[i, j]).DefineLandTypeRandomly(ref barracksCount, ref recyclingSiteCount, ref electronicDumpCount);

                    //if (((Land)LatitudLongitud[i, j]).LandType == EnumLandType.barrack) barracksCount--;
                    //if (((Land)LatitudLongitud[i, j]).LandType == EnumLandType.recyclingSite) recyclingSiteCount--;
                    //if (((Land)LatitudLongitud[i, j]).LandType == EnumLandType.electronicDump) electronicDumpCount--;
                }
            }
        }

        static public bool CheckSameLocation(Location locationA, Location locationB)
        {
            return (locationA.Latitud == locationB.Latitud && locationA.Longitud == locationB.Longitud);
        }

        static public List<Location> LandTypeLocation(EnumLandType landType)
        {
            List<Location> locations = new List<Location>();
            
            for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
                {
                    if (((Land)Map.LatitudLongitud[i, j]).LandType == landType)
                        locations.Add(Map.LatitudLongitud[i, j]);
                }
            }
            return locations;
        }

        static public Location ClosestLandTypeLocation(Operator operatorx, EnumLandType landType)
        {
            Location closestLocation = operatorx.Location;
            int steps = LatitudLongitud.GetLength(0) * LatitudLongitud.GetLength(1);

            foreach (Location location in LandTypeLocation(landType))
            {
                Stack<Land> journeySteps = JourneySteps(operatorx, location, true);
                if(journeySteps.Count() > 0 && journeySteps.Count() < steps)
                {
                    steps = journeySteps.Count()-1;
                    closestLocation = location;
                }
                    
            }
            Console.WriteLine(steps);
            return closestLocation;
        }

         static public bool LocationEntryValidation(out Location location)
        {
            string message = "";
            location = new Location(-1, -1);

            if (!int.TryParse(CommonFunctions.UserInput("Indique latitud:"), out int latitud))
                message += "la latitud no es un número\n";
            if (!int.TryParse(CommonFunctions.UserInput("Indique longitud:"), out int longitud))
                message += "la longitud no es un número\n";

            if (message == "")
                location = new Location(latitud, longitud);

            if (!CheckLocationWithinMapLimits(location))
                message += $"La latitud debe ser un nùmero entre 0 y {LatitudLongitud.GetLength(0)-1} y la longitud debe ser un nùmero entre 0 y {LatitudLongitud.GetLength(1)-1}\n\n";

            CommonFunctions.IfErrorMessageShowIt(message);

            return message == "";
        }

         static private bool CheckLocationWithinMapLimits(Location location)
        {
            bool latitudOK = location.Latitud >= 0 && location.Latitud < LatitudLongitud.GetLength(0);
            bool longitudOK = location.Longitud >= 0 && location.Longitud < LatitudLongitud.GetLength(1);

            return latitudOK && longitudOK;
        }

        static public int CalculateDistance(Location origin, Location destination)
        {
            int verticalMovement = Math.Abs(destination.Latitud - origin.Latitud);
            int horizontalMovement = Math.Abs(destination.Longitud - origin.Longitud);

            return verticalMovement + horizontalMovement;
        }

        static public EnumLandType CheckLandType(Location location)
        {
            return ((Land)LatitudLongitud[location.Latitud, location.Longitud]).LandType;
        }

        //////////////////////////////////////////////
        /* RUTINAS DE RECORRIDO
        Recorrido:
        1- identifica el origen y el destino
        2- genera un submapa[,] desde origen a destino (tomando como àngulo superior izquierdo la mìnima latitud y longitud y como extensiòn la distancia absoluta entre latitudes y longitudes del origen y destino.
        3- para que el recorrido sea siempre de izquierda a derecha y de arriba hacia abajo, se espeja dicho mapa para espejarlo y que el origen quede en el àngulo superior izquierdo y el destino en el àngulo inferior derecho. ya que al fin y al cabo el recorrido a realizar es el mismo.
        4- se genera una diccionario con todos los "kilòmetros cuadrados" que conforman ese submapa
        5 - Se eliminan aquellos que no se desea usar (para los que no pueden pasar por los lagos, se eliminan los lagos, y en caso de optar por camino òptimo los electronicDump que dañan a los operadores.
        6- se van dando pasos desde el origen al destino, siempre probando primero hacia abajo y luego a la derecha, etc,  y se los guarda en una pila. Si desde donde se està parado no existe un paso adicional a dar, se "va para atras" quitando de a uno los pasos e intentando encontrar algùn paso disponible hacia el destino. Mientras se guarda el recorrido realizado.
        7- faltarìa usar un patron de diseño para intercalar que se elijan los pasos primero de abajo y luego a la derecha, y luego que el orden se invierta, buscar primero si existe la opciòn de ir a la derecha y luego si no la hay hacia abajo. La idea con esto es siempre ir en diagonal, ya que en este caso se està siempre en el medio del mapa lo que permite esquivar lugares no deseados en menos pasos. Es decir, no sòlo que el camino sea óptimo en el sentido de evitar problemas sino también que sea eficiente en todos los casos, o sea, el más corto.
        // queda pendiente lógica de punto de partida y final
        */

        ///Paso 6
        static public Stack<Land> JourneySteps(Operator operatorx, Location destination, bool optimize)
        {
            Dictionary<string, Land> tripPosibleStepsOptimum = TripPosibleStepsOptimum(operatorx, destination, optimize);

            Stack<Land> journey = new Stack<Land>();
            Stack<Location> steps = new Stack<Location>();

            Land landStep;
            steps.Push(new Location(0, 0));  //siempre parte de este punto ya q es un submapa del viaje
            journey.Push(tripPosibleStepsOptimum.First().Value);  //incorpora el primer paso al viaje
            tripPosibleStepsOptimum.Remove(tripPosibleStepsOptimum.First().Key); //y lo elimina de los posibles pasos a dar a futuro 


            while (journey.Count > 0 && !(journey.Peek().Latitud == destination.Latitud && journey.Peek().Longitud == destination.Longitud))
            {
                // paso al sur
                if (tripPosibleStepsOptimum.Remove((steps.Peek().Latitud + 1) + "-" + steps.Peek().Longitud, out landStep))
                {
                    journey.Push(landStep);
                    steps.Push(new Location(steps.Peek().Latitud + 1, steps.Peek().Longitud));
                    // Control se puede aplicar al resto cambiando el punto cardinal
                    //Console.WriteLine("diste un paso en sur");
                    //Console.WriteLine(journey.Peek());
                    //Console.WriteLine(steps.Peek());
                }
                //paso al este
                else if (tripPosibleStepsOptimum.Remove(steps.Peek().Latitud + "-" + (steps.Peek().Longitud + 1), out landStep))
                {
                    journey.Push(landStep);
                    steps.Push(new Location(steps.Peek().Latitud, steps.Peek().Longitud + 1));
                }
                //paso al norte
                else if (tripPosibleStepsOptimum.Remove((((uint)steps.Peek().Latitud) - 1) + "-" + steps.Peek().Longitud, out landStep))
                {
                    journey.Push(landStep);
                    steps.Push(new Location(steps.Peek().Latitud - 1, steps.Peek().Longitud));
                }
                //paso al oeste
                else if (tripPosibleStepsOptimum.Remove(steps.Peek().Latitud + "-" + (steps.Peek().Longitud - 1), out landStep))
                {
                    journey.Push(landStep);
                    steps.Push(new Location(steps.Peek().Latitud, steps.Peek().Longitud - 1));
                }
                else
                {
                    journey.Pop();
                    steps.Pop();
                }
            }
            return journey;
        }

        static public void JourneyStepsPrint(Operator operatorx, Location destination, bool optimize)
        {
            foreach (Land land in JourneySteps(operatorx, destination, optimize).Reverse())
                Console.WriteLine(land);
        }


        ///Paso 5
        static private Dictionary<string, Land> TripPosibleStepsOptimum(Operator operatorx, Location destination, bool optimize)
        {
            Dictionary<string, Land> tripPosibleStepsOptimum = TripPosibleSteps(operatorx, destination);

            if (TryTripPosibleStepsOptimum(operatorx, destination, optimize))
                RemoveDangerousSteps(operatorx, tripPosibleStepsOptimum, optimize);

            //imprime lista de tierras
            //Console.Write("\n");
            //foreach (KeyValuePair<string, Land> land in tripPosibleStepsOptimum)
            //    Console.WriteLine(tripPosibleStepsOptimum.Key + " - " + tripPosibleStepsOptimum.Value);

            return tripPosibleStepsOptimum;
        }

        static private bool TryTripPosibleStepsOptimum(Operator operatorx, Location destination, bool optimize)
        {
            Dictionary<string, Land> tripPosibleSteps = TripPosibleSteps(operatorx, destination);

            string message = "";
            if (operatorx is OperatorM8 && tripPosibleSteps.Last().Value.LandType == EnumLandType.lake)
                message += "No es posible realizar el vieaje pq el destino es un lago y este operador no puede caminar sobre el agua\n";
            if (operatorx is OperatorK9 && tripPosibleSteps.Last().Value.LandType == EnumLandType.lake)
                message += "No es posible realizar el viaje pq el destino es un lago y este operador no puede caminar sobre el agua\n";
            if (optimize && tripPosibleSteps.Last().Value.LandType == EnumLandType.electronicDump)
                Console.WriteLine("Marcaste recorrido sin riesgos pero el destino seleccionado es un lugar riesgoso (vertedero electrónico), por lo tanto estás sometido a dicho riesgo\n");
            if (optimize && tripPosibleSteps.Last().Value.LandType == EnumLandType.electronicDump)
                Console.WriteLine("Marcaste recorrido sin riesgos pero el destino seleccionado es un lugar riesgoso (vertedero), por lo tanto estás sometido a dicho riesgo\n");

            if (message != "") Console.WriteLine(message);

            return message == "";
        }

        static private void RemoveDangerousSteps(Operator operatorx, Dictionary<string, Land> tripPosibleStepsOptimum, bool optimize)
        {
            foreach (KeyValuePair<string, Land> step in tripPosibleStepsOptimum)
            {
                if (step.Key != tripPosibleStepsOptimum.First().Key && step.Key != tripPosibleStepsOptimum.Last().Key)
                {
                    if (operatorx is OperatorM8 && step.Value.LandType == EnumLandType.lake)
                        tripPosibleStepsOptimum.Remove(step.Key);
                    if (operatorx is OperatorK9 && step.Value.LandType == EnumLandType.lake)
                        tripPosibleStepsOptimum.Remove(step.Key);
                    if (step.Value.LandType == EnumLandType.electronicDump && optimize)
                        tripPosibleStepsOptimum.Remove(step.Key);
                    if (step.Value.LandType == EnumLandType.dump && optimize)
                        tripPosibleStepsOptimum.Remove(step.Key);
                }
            }
        }

        ///Paso 4
        static private Dictionary<string, Land> TripPosibleSteps(Operator operatorx, Location destination)
        {
            Land[,] tripSubMapOrdered = TripSubMapOrdered(operatorx, destination);
            Dictionary<string, Land> tripPosibleSteps = new Dictionary<string, Land>();

            for (int i = 0; i < tripSubMapOrdered.GetLength(0); i++)
            {
                for (int j = 0; j < tripSubMapOrdered.GetLength(1); j++)
                {
                    tripPosibleSteps.Add(i + "-" + j, tripSubMapOrdered[i, j]);
                }
            }

            return tripPosibleSteps;
        }


        /// Paso 3
        static private Land[,] TripSubMapOrdered(Operator operatorx, Location destination)
        {
            Land[,] tripSubMap = TripSubMap(operatorx, destination);
        Land[,] tripSubMapOrdered;

            if (destination.Latitud == operatorx.Location.Latitud && destination.Longitud<operatorx.Location.Longitud)
                tripSubMapOrdered = TripSubMapOrderedWest(tripSubMap);
            else if (destination.Latitud<operatorx.Location.Latitud && destination.Longitud == operatorx.Location.Longitud)
                tripSubMapOrdered = TripSubMapOrderedSouth(tripSubMap);
            else if (destination.Latitud > operatorx.Location.Latitud && destination.Longitud<operatorx.Location.Longitud)
                tripSubMapOrdered = TripSubMapOrderedSouthWest(tripSubMap);
            else if (destination.Latitud<operatorx.Location.Latitud && destination.Longitud> operatorx.Location.Longitud)
                tripSubMapOrdered = TripSubMapOrderedNorthEast(tripSubMap);
            else if (destination.Latitud<operatorx.Location.Latitud && destination.Longitud<operatorx.Location.Longitud)
                tripSubMapOrdered = TripSubMapOrderedNorthWest(tripSubMap);
            else
                tripSubMapOrdered = tripSubMap;

            //imprime mapaReordenado
            //Console.Write("\n");
            //for (int i = 0; i < tripSubMapOrdered.GetLength(0); i++)
            //{
            //    Console.Write("\n");
            //    for (int j = 0; j < tripSubMapOrdered.GetLength(1); j++)
            //    {
            //        Console.Write(tripSubMapOrdered[i, j].LandType.ToString()[0]);
            //    }
            //}

            return tripSubMapOrdered;
        }

        static private Land[,] TripSubMapOrderedWest(Land[,] tripSubMap)
        {
            Land[,] tripSubMapOrdered = new Land[tripSubMap.GetLength(0), tripSubMap.GetLength(1)];

            for (int j = 0; j < tripSubMap.GetLength(1); j++)
            {
                tripSubMapOrdered[0, tripSubMap.GetLength(1) - 1 - j] = tripSubMap[0, j];
            }

            return tripSubMapOrdered;
        }

        static private Land[,] TripSubMapOrderedSouth(Land[,] tripSubMap)
        {
            Land[,] tripSubMapOrdered = new Land[tripSubMap.GetLength(0), tripSubMap.GetLength(1)];

            for (int i = 0; i < tripSubMap.GetLength(0); i++)
            {
                tripSubMapOrdered[tripSubMap.GetLength(0) - 1 - i, 0] = tripSubMap[i, 0];
            }

            return tripSubMapOrdered;
        }

        static private Land[,] TripSubMapOrderedSouthWest(Land[,] tripSubMap)
        {
            Land[,] tripSubMapOrdered = new Land[tripSubMap.GetLength(0), tripSubMap.GetLength(1)];

            for (int i = 0; i < tripSubMap.GetLength(0); i++)
            {
                for (int j = 0; j < tripSubMap.GetLength(1); j++)
                {
                    tripSubMapOrdered[i, tripSubMap.GetLength(1) - 1 - j] = tripSubMap[i, j];
                }
            }

            return tripSubMapOrdered;
        }

        static private Land[,] TripSubMapOrderedNorthEast(Land[,] tripSubMap)
        {
            Land[,] tripSubMapOrdered = new Land[tripSubMap.GetLength(0), tripSubMap.GetLength(1)];

            for (int i = 0; i < tripSubMap.GetLength(0); i++)
            {
                for (int j = 0; j < tripSubMap.GetLength(1); j++)
                {
                    tripSubMapOrdered[tripSubMap.GetLength(0) - 1 - i, j] = tripSubMap[i, j];
                }
            }

            return tripSubMapOrdered;
        }

        static private Land[,] TripSubMapOrderedNorthWest(Land[,] tripSubMap)
        {
            Land[,] tripSubMapOrdered = new Land[tripSubMap.GetLength(0), tripSubMap.GetLength(1)];

            for (int i = 0; i < tripSubMap.GetLength(0); i++)
            {
                for (int j = 0; j < tripSubMap.GetLength(1); j++)
                {
                    tripSubMapOrdered[tripSubMap.GetLength(0) - 1 - i, tripSubMap.GetLength(1) - 1 - j] = tripSubMap[i, j];
                }
            }

            return tripSubMapOrdered;
        }

        ///Pasos 1 y 2
        static private Land[,] TripSubMap(Operator operatorx, Location destination)
        {
            Land[,] tripSubMap = new Land[Math.Abs(destination.Latitud - operatorx.Location.Latitud) + 1, Math.Abs(destination.Longitud - operatorx.Location.Longitud) + 1];

            int topLeftCornerLatitud = Math.Min(operatorx.Location.Latitud, destination.Latitud);
            int topLeftCornerLongitud = Math.Min(operatorx.Location.Longitud, destination.Longitud);

            for (int i = 0; i < tripSubMap.GetLength(0); i++)
            {
                for (int j = 0; j < tripSubMap.GetLength(1); j++)
                {
                    tripSubMap[i, j] = (Land)LatitudLongitud[topLeftCornerLatitud + i, topLeftCornerLongitud + j];
                }
            }

            //imprime submapa
            //for (int i = 0; i < tripSubMap.GetLength(0); i++)
            //{
            //    Console.Write("\n");
            //    for (int j = 0; j < tripSubMap.GetLength(1); j++)
            //    {
            //        Console.Write(tripSubMap[i, j].LandType.ToString()[0]);
            //    }
            //}

            return tripSubMap;
        }
    }
}
