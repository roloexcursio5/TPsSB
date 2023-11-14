namespace TPI
{
    internal class Location
    {
        public int Latitud { get; set; }
        public int Longitud { get; set; }

        //public int latitud;
        //public int longitud;

        public Location(int latitud, int longitud)
        {
            this.Latitud = latitud;
            this.Longitud = longitud;
        }

        public bool CheckSameLocation(Operator operatorTheOther)
        {
            return (Latitud == operatorTheOther.Location.Latitud && Longitud == operatorTheOther.Location.Longitud);
        }

        public override string ToString()
        {
            return $"latitud: {Latitud} y longitud: {Longitud}";
        }
    }
}