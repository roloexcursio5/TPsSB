namespace TPI
{
    internal class Land : Location
    {
        public EnumLandType LandType { get; set; }
        public Land(int latitud, int longitud) : base(latitud,longitud)
        {
        }

        public void DefineLandTypeRandomly(ref int barracksCount, ref int recyclingSiteCount, ref int electronicDumpCount)
        {
            Random random = new Random();
            int index = random.Next(0, 101);

            if (index == 0 && barracksCount > 0)
            { 
                LandType = EnumLandType.barrack;
                barracksCount--;
            }
            else if (index <= 1 && index < 5 && recyclingSiteCount > 0)
            { 
                LandType = EnumLandType.recyclingSite;
                recyclingSiteCount--;
            }
            else if (index <= 5 && index < 10 && electronicDumpCount > 0)
            { 
                LandType = EnumLandType.electronicDump;
                electronicDumpCount--;
            }
            else if (index <= 10 && index < 20)
                LandType = EnumLandType.wasteland;
            else if (index <= 20 && index < 30)
                LandType = EnumLandType.dump;
            else if (index <= 30 && index < 40)
                LandType = EnumLandType.lake;
            else if (index <= 40 && index < 60)
                LandType = EnumLandType.urbanArea;
            else if (index <= 60 && index < 80)
                LandType = EnumLandType.forest;
            else
                LandType = EnumLandType.plain;
        }

        public override string ToString()
        {
            return $"latitud: {Latitud} y longitud: {Longitud} - {LandType}";
        }
    }
}
