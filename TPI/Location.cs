namespace TPI
{
    internal class Location
    {
        public int Latitud { get; set; }
        public int Longitud { get; set; }

        public Location(int Latitud, int Longitud)
        {
            this.Latitud = Latitud;
            this.Longitud = Longitud;
        }

        public override string ToString()
        {
            return $"latitud: {Latitud} y longitud: {Longitud}";
        }
    }
}