namespace TPI
{
    internal class Map
    {
        public Location[,] LatitudLongitud { get; set; } = new Location[100, 100];
        //public Location[,] latitudLongitud = new Location[100, 100];

        public Map()
        {
            CreateMapRandomly();
         }

        private void CreateMapRandomly()
        {
            int barracksCount = 1;
            int recyclingSiteCount = 3;
            int electronicDumpCount = 100;

            for (int i = 0; i < LatitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < LatitudLongitud.GetLength(1); j++)
                {
                    LatitudLongitud[i, j] = new Land(i, j);
                    ((Land)LatitudLongitud[i, j]).DefineLandTypeRandomly(barracksCount, recyclingSiteCount, electronicDumpCount);

                    if (((Land)LatitudLongitud[i, j]).LandType == LandType.barracks) barracksCount--;
                    if (((Land)LatitudLongitud[i, j]).LandType == LandType.recyclingSite) recyclingSiteCount--;
                    if (((Land)LatitudLongitud[i, j]).LandType == LandType.electronicDump) electronicDumpCount--;
                }
            }
        }


        public bool CheckMapValidLocation(string latitudInput, string longitudInput)
        {
            string message = "";

            if (!int.TryParse(latitudInput, out int latitud))
                message += "la latitud no es un número";
            if (!int.TryParse(longitudInput, out int longitud))
                message += "la longitud no es un número";

            if (message == "")
            {
                Location location = new Location(latitud, longitud);
                if (!CheckLocationWithinMapLimits(location))
                    message += $"La latitud debe ser un nùmero entre 0 y {LatitudLongitud.GetLength(0)} y la longitud debe ser un nùmero entre 0 y {LatitudLongitud.GetLength(1)}\n\n";
            }

            if (message != "")
                Console.WriteLine(message);

            return message == "";
        }


        private bool CheckLocationWithinMapLimits(Location location)
        {
            bool latitudOK = location.Latitud >= 0 && location.Latitud < LatitudLongitud.GetLength(0);
            bool longitudOK = location.Longitud >= 0 && location.Longitud < LatitudLongitud.GetLength(1);

            return latitudOK && longitudOK;
        }

    }
}
