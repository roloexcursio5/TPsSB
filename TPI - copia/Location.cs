namespace TPI
{
    internal class Location
    {
        public int Latitud { get; set; }
        public int Longitud { get; set; }

        public Location(int latitud, int longitud)
        {
            this.Latitud = latitud;
            this.Longitud = longitud;
        }

        public bool CheckSameLocation(Location locationToCompare)
        {
            return (Latitud == locationToCompare.Latitud && Longitud == locationToCompare.Longitud);
        }

        public override string ToString()
        {
            return $"latitud: {Latitud} y longitud: {Longitud}";
        }
    }
}