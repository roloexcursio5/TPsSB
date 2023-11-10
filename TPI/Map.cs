namespace TPI
{
    internal class Map
    {
        public Location[,] latitudLongitud = new Location[100, 100];

        public Map()
        {
            CreateMapRandomly();
         }

        private void CreateMapRandomly()
        {
            int barracksCount = 1;
            int recyclingSiteCount = 3;
            int electronicDumpCount = 100;

            for (int i = 0; i < latitudLongitud.GetLength(0); i++)
            {
                for (int j = 0; j < latitudLongitud.GetLength(1); j++)
                {
                    latitudLongitud[i, j] = new Land(i, j);
                    ((Land)latitudLongitud[i, j]).DefineLandTypeRandomly(barracksCount, recyclingSiteCount, electronicDumpCount);

                    if (((Land)latitudLongitud[i, j]).type == LandType.barracks) barracksCount--;
                    if (((Land)latitudLongitud[i, j]).type == LandType.recyclingSite) recyclingSiteCount--;
                    if (((Land)latitudLongitud[i, j]).type == LandType.electronicDump) electronicDumpCount--;
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
                    message += $"La latitud debe ser un nùmero entre 0 y {latitudLongitud.GetLength(0)} y la longitud debe ser un nùmero entre 0 y {latitudLongitud.GetLength(1)}\n\n";
            }

            if (message != "")
                Console.WriteLine(message);

            return message == "";
        }


        private bool CheckLocationWithinMapLimits(Location location)
        {
            bool latitudOK = location.latitud >= 0 && location.latitud < latitudLongitud.GetLength(0);
            bool longitudOK = location.longitud >= 0 && location.longitud < latitudLongitud.GetLength(1);

            return latitudOK && longitudOK;
        }

    }
}
