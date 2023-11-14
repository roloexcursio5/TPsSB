namespace TPI
{
    internal class Land : Location
    {
        public LandType LandType { get; set; }
        //public LandType type;

        public Land(int latitud, int longitud) : base(latitud,longitud)
        {
        }

        public void DefineLandTypeRandomly(int barracksCount, int recyclingSiteCount, int electronicDumpCount)
        {
            Random random = new Random();
            int index = random.Next(0, 101);

            if (index == 0 && barracksCount > 0)
                LandType = LandType.barracks;
            else if (index <= 1 && index < 5 && recyclingSiteCount > 0)
                LandType = LandType.recyclingSite;
            else if (index <= 5 && index < 10 && electronicDumpCount > 0)
                LandType = LandType.electronicDump;
            else if (index <= 10 && index < 20)
                LandType = LandType.wasteland;
            else if (index <= 20 && index < 30)
                LandType = LandType.dump;
            else if (index <= 30 && index < 40)
                LandType = LandType.lake;
            else if (index <= 40 && index < 60)
                LandType = LandType.urbanArea;
            else if (index <= 60 && index < 80)
                LandType = LandType.forest;
            else
                LandType = LandType.plain;
        }

    }
}
