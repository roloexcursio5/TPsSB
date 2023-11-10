namespace TPI
{
    internal class Land : Location
    {
        public LandType type;

        public Land(int latitud, int longitud) : base(latitud,longitud)
        {
        }

        public void DefineLandTypeRandomly(int barracksCount, int recyclingSiteCount, int electronicDumpCount)
        {
            Random random = new Random();
            int index = random.Next(0, 101);

            if (index == 0 && barracksCount > 0)
                type = LandType.barracks;
            else if (index <= 1 && index < 5 && recyclingSiteCount > 0)
                type = LandType.recyclingSite;
            else if (index <= 5 && index < 10 && electronicDumpCount > 0)
                type = LandType.electronicDump;
            else if (index <= 10 && index < 20)
                type = LandType.wasteland;
            else if (index <= 20 && index < 30)
                type = LandType.dump;
            else if (index <= 30 && index < 40)
                type = LandType.lake;
            else if (index <= 40 && index < 60)
                type = LandType.urbanArea;
            else if (index <= 60 && index < 80)
                type = LandType.forest;
            else
                type = LandType.plain;
        }

    }
}
