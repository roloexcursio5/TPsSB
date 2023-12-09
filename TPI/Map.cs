namespace TPI
{
    internal static class Map
    {
        static public Location[,] LatitudLongitud { get; set; } = new Location[100, 100];

        static public void CreateMapRandomly()
        {
            int barracksCount = 1;
            int recyclingSiteCount = 5;
            int electronicDumpCount = 100;

            for (int i = 0; i < LatitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < LatitudLongitud.GetLength(1); j++)
                {
                    LatitudLongitud[i, j] = new Land(i, j);
                    ((Land)LatitudLongitud[i, j]).DefineLandTypeRandomly(ref barracksCount, ref recyclingSiteCount, ref electronicDumpCount);
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
            
            for (int i = 0; i < LatitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < LatitudLongitud.GetLength(1); j++)
                {
                    if (((Land)LatitudLongitud[i, j]).LandType == landType)
                        locations.Add(LatitudLongitud[i, j]);
                }
            }
            return locations;
        }

        static public Location ClosestLandTypeLocation(Operator operatorx, EnumLandType landType)
        {
            Location closestLocation = operatorx.Location;
            int steps = LatitudLongitud.GetLength(0) * LatitudLongitud.GetLength(1); // se pone el màximo de pasos a dar como punto de partida
            List<Location> posiblesDestinations = LandTypeLocation(landType);
            Console.WriteLine($"Hay {posiblesDestinations.Count()} {landType}");

            foreach (Location location in posiblesDestinations)
            {
                Stack<Land> trip = Trip(operatorx, location, true);
                if (trip.Count() > 0 && trip.Count() < steps)
                {
                    steps = trip.Count() - 1;
                    closestLocation = location;
                }

            }
            if(steps < LatitudLongitud.GetLength(0) * LatitudLongitud.GetLength(1))
                Console.WriteLine($"el màs cerca està a {steps} kilometros");
            else
                Console.WriteLine("no se encontrò un camino optimo màs cercano que se pueda realizar");
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

        // PARA IMPLEMENTAR A* COSA QUE AQUÌ NO SE HIZO PQ SE INTENTÒ RESOLVER DE OTRO MODO, SEGURAMENTE INCORRECTO, JA
        //static public int CalculateDistance(Location origin, Location destination)
        //{
        //    int verticalMovement = Math.Abs(destination.Latitud - origin.Latitud);
        //    int horizontalMovement = Math.Abs(destination.Longitud - origin.Longitud);

        //    return verticalMovement + horizontalMovement;
        //}

        static public EnumLandType CheckLandType(Location location)
        {
            return ((Land)LatitudLongitud[location.Latitud, location.Longitud]).LandType;
        }

        //////////////////////////////////////////////
        /* RUTINAS DE RECORRIDO
        INTENTÈ RESOLVERLO. LUEGO EN CLASE COMENTARON DE A* PERO YA ESTABA DESARROLLADO ESTO. QUEDA PENDIENTE IMPLEMENTAR ESE ALGORÍTMO
        Recorrido:
        1- identifica el origen y el destino. Genera un submapa[,] desde origen a destino (tomando como àngulo superior izquierdo la mìnima latitud y longitud y como extensiòn la distancia absoluta entre latitudes y longitudes del origen y destino. De modo de seguir siempre una rutina de pasos y de lograr que se haga un embudo hasta el destino.
        2- para que el recorrido sea siempre de izquierda a derecha y de arriba hacia abajo, se espeja dicho mapa para espejarlo y que el origen quede en el àngulo superior izquierdo y el destino en el àngulo inferior derecho. ya que al fin y al cabo el recorrido a realizar es el mismo.
        3- se genera una diccionario con todos los "kilòmetros cuadrados" que conforman ese submapa
        4 - Se eliminan aquellos que no se desea usar (para los que no pueden pasar por los lagos, se eliminan los lagos, y en caso de optar por camino òptimo los electronicDump que dañan a los operadores.
        5- se van dando pasos desde el origen al destino, siempre probando primero hacia abajo y luego a la derecha, etc,  y se los guarda en una pila. Si desde donde se està parado no existe un paso adicional a dar, se "va para atras" quitando de a uno los pasos e intentando encontrar algùn paso disponible hacia el destino. Mientras se guarda el recorrido realizado.
        X- faltarìa usar un patron de diseño para intercalar que se elijan los pasos primero de abajo y luego a la derecha, y luego que el orden se invierta, buscar primero si existe la opciòn de ir a la derecha y luego si no la hay hacia abajo. La idea con esto es siempre ir en diagonal, ya que en este caso se està siempre en el medio del mapa lo que permite esquivar lugares no deseados en menos pasos. Es decir, no sòlo que el camino sea óptimo en el sentido de evitar problemas sino también que sea eficiente en todos los casos, o sea, el más corto.
        // queda pendiente lógica de punto de partida y final A*
        */


        ////////////////////////////////////////////////////////////
        static public Stack<Land> Trip(Operator operatorx, Location destination, bool optimize) // por el momento està todo en optimo pero FALTA hacerlo optativo
        {
            Stack<Land> journeyStep5 = new Stack<Land>();

            if (TryTrip(operatorx, destination, optimize))
            {
                Land[,] mapStep1 = TripSubMap(operatorx, destination);
                Land[,] mapStep2 = TripSubMapOrdered(mapStep1, operatorx, destination);
                Dictionary<string, Land> tripStepsDictionaryStep3 = TripStepsDictionary(mapStep2); // se crea una especia de mapa virtual
                Dictionary<string, Land> tripStepsDictionaryStep4 = new Dictionary<string, Land>(tripStepsDictionaryStep3); // especie de back up con fines de tener tripStepsDictionaryStep3 para control
                RemoveDangerousSteps(operatorx, tripStepsDictionaryStep4, optimize);

                journeyStep5 = TripJourneySteps(tripStepsDictionaryStep4, destination);

                ////imprime lista de tierras
                //Console.Write("\n");
                //    foreach (KeyValuePair<string, Land> land in tripStepsDictionaryStep4)
                //        Console.WriteLine(land.Key + " - " + land.Value);
            }

            return journeyStep5;
        }
        //////////////////////////////////////////////////////////



        ///Paso 5
        static private Stack<Land> TripJourneySteps(Dictionary<string, Land> tripStepsDictionaryStep4, Location destination)
        {
            Stack<Land> journey = new Stack<Land>();        //para seguir el recorrido a los efectos del juego
            Stack<Location> steps = new Stack<Location>();  //para seguir el recorrido de los pasos que se dió en caso de tener que volver hacia atras. Recordar que en esta instancia se dan pasos en un mapa virtual

            Land landStep;
            steps.Push(new Location(0, 0));  //siempre parte de este punto ya q es un submapa virtual del viaje
            journey.Push(tripStepsDictionaryStep4.First().Value);  //incorpora el primer paso al viaje
            tripStepsDictionaryStep4.Remove(tripStepsDictionaryStep4.First().Key); //y lo elimina de los posibles pasos a dar a futuro 


            while (journey.Count > 0 && !(journey.Peek().Latitud == destination.Latitud && journey.Peek().Longitud == destination.Longitud))  // mientras desde el principio haya pasos para dar (pq el pop puede incluso sacarte el punto de partida de donde està el operador y eso significa que te quedaste sin pasos para dar y por tanto el viaje no se puede hacer) y que aùn no se haya llegado a destino se sigue el while
            {
                // paso al sur
                if (tripStepsDictionaryStep4.Remove((steps.Peek().Latitud + 1) + "-" + steps.Peek().Longitud, out landStep)) // este remove es una sobrecarga del mètodo que no solo te dice si existe para eliminar con un boolean, sino que ademas lo remueve, pero te guarda el valor del diccionario (no la key) en el out landStep (sì, pensaron en todo)
                {
                    journey.Push(landStep);  // agrega paso real del mapa al sur (dictinary value)
                    steps.Push(new Location(steps.Peek().Latitud + 1, steps.Peek().Longitud)); // guarda el paso virtual (dictionary key)
                    // Control se puede aplicar al resto cambiando el punto cardinal
                    //Console.WriteLine("diste un paso en sur");
                    //Console.WriteLine(journey.Peek());
                    //Console.WriteLine(steps.Peek());
                }
                //paso al este
                else if (tripStepsDictionaryStep4.Remove(steps.Peek().Latitud + "-" + (steps.Peek().Longitud + 1), out landStep))
                {
                    journey.Push(landStep);
                    steps.Push(new Location(steps.Peek().Latitud, steps.Peek().Longitud + 1));
                }
                //paso al norte
                else if (tripStepsDictionaryStep4.Remove((((uint)steps.Peek().Latitud) - 1) + "-" + steps.Peek().Longitud, out landStep))
                {
                    journey.Push(landStep);
                    steps.Push(new Location(steps.Peek().Latitud - 1, steps.Peek().Longitud));
                }
                //paso al oeste
                else if (tripStepsDictionaryStep4.Remove(steps.Peek().Latitud + "-" + (steps.Peek().Longitud - 1), out landStep))
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

        ///Paso 4
        static private void RemoveDangerousSteps(Operator operatorx, Dictionary<string, Land> tripPosibleStepsOptimum, bool optimize)
        {
            foreach (KeyValuePair<string, Land> step in tripPosibleStepsOptimum)
            {
                if (step.Key != tripPosibleStepsOptimum.First().Key && step.Key != tripPosibleStepsOptimum.Last().Key)
                {
                    if ((operatorx is OperatorM8 || operatorx is OperatorK9) && step.Value.LandType == EnumLandType.lake)
                        tripPosibleStepsOptimum.Remove(step.Key);
                    if (step.Value.LandType == EnumLandType.electronicDump && optimize)
                        tripPosibleStepsOptimum.Remove(step.Key);
                }
            }
        }

        ///Paso 3
        static private Dictionary<string, Land> TripStepsDictionary(Land[,] mapStep2)
        {
            Dictionary<string, Land> tripStepsDictionary = new Dictionary<string, Land>();

            for (int i = 0; i < mapStep2.GetLength(0); i++)
            {
                for (int j = 0; j < mapStep2.GetLength(1); j++)
                {
                    tripStepsDictionary.Add(i + "-" + j, mapStep2[i, j]); //esto genera un mapa virtual donde las coordenadas de las key del diccionario en verdad son para hacer el viaje pero el verdadelo valor de los pasos del viaje estàn en el value del diccionario
                }
            }

            return tripStepsDictionary;
        }

        /// Paso 2
        static private Land[,] TripSubMapOrdered(Land[,] mapStep1, Operator operatorx, Location destination)
        {
            if (destination.Latitud == operatorx.Location.Latitud && destination.Longitud < operatorx.Location.Longitud)
                mapStep1 = TripSubMapOrderedWest(mapStep1);
            else if (destination.Latitud < operatorx.Location.Latitud && destination.Longitud == operatorx.Location.Longitud)
                mapStep1 = TripSubMapOrderedSouth(mapStep1);
            else if (destination.Latitud > operatorx.Location.Latitud && destination.Longitud < operatorx.Location.Longitud)
                mapStep1 = TripSubMapOrderedSouthWest(mapStep1);
            else if (destination.Latitud < operatorx.Location.Latitud && destination.Longitud > operatorx.Location.Longitud)
                mapStep1 = TripSubMapOrderedNorthEast(mapStep1);
            else if (destination.Latitud < operatorx.Location.Latitud && destination.Longitud < operatorx.Location.Longitud)
                mapStep1 = TripSubMapOrderedNorthWest(mapStep1);
            else
                mapStep1 = mapStep1;

            ////imprime mapaReordenado
            //Console.Write("\n");
            //for (int i = 0; i < tripSubMapOrdered.GetLength(0); i++)
            //{
            //    Console.Write("\n");
            //    for (int j = 0; j < tripSubMapOrdered.GetLength(1); j++)
            //    {
            //        Console.Write(tripSubMapOrdered[i, j].LandType.ToString()[0]);
            //    }
            //}

            return mapStep1;
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

        ///Paso 1
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

        static private bool TryTrip(Operator operatorx, Location destination, bool optimize)
        {
            string message = "";
            if ((operatorx is OperatorK9 || operatorx is OperatorM8) && ((Land)LatitudLongitud[destination.Latitud, destination.Longitud]).LandType == EnumLandType.lake)
                message += "No es posible realizar el viaje pq el destino es un lago y este operador no puede caminar sobre el agua\n";
            if (optimize && ((Land)LatitudLongitud[destination.Latitud, destination.Longitud]).LandType == EnumLandType.electronicDump)
                Console.WriteLine("No es posible realizar el viaje pq el destino es un lugar riesgoso (vertedero electrónico) y marcaste recorrido sin riesgos\n");

            CommonFunctions.IfErrorMessageShowIt(message);

            return message == "";
        }

        /// métodos genéricos para imprimir
        static public string ToString(int Latitud, int Longitud)
        {
            return $"{Latitud},{Longitud},'{((Land)LatitudLongitud[Latitud, Longitud]).LandType}'";
        }

        static public void MapPrint()
        {
            for (int i = 0; i < Map.LatitudLongitud.GetLength(0); i++)
            {
                Console.Write("\n");
                for (int j = 0; j < Map.LatitudLongitud.GetLength(1); j++)
                {
                    Console.Write(((Land)Map.LatitudLongitud[i, j]).LandType.ToString()[0]);
                }
            }
        }


        // para guardar el juego y luego recuperarlo por temas de que JsonConvert no trabaja con matriz de doble entrada que es como està hecho el mapa
        public static List<Land[]> MapArrayToList()
        {
            List<Land[]> mapASAList = new List<Land[]>();

            for (int i = 0; i < LatitudLongitud.GetLength(0); i++)
            {
                Land[] mapRowArray = new Land[LatitudLongitud.GetLength(1)];
                for (int j = 0; j < mapRowArray.Length; j++)
                {
                    mapRowArray[j] = (Land)LatitudLongitud[i, j];
                }

                mapASAList.Add(mapRowArray);
            }

            return mapASAList;
        }

        public static Land[,] MapListToArray(List<Land[]> mapASAList)
        {
            Land[,] map = new Land[mapASAList.Count, mapASAList[0].Length];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = mapASAList[i][j];
                }
            }

            return map;
        }
    }
}
