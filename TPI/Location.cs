namespace TPI
{
    internal class Location
    {
        public int latitud;
        public int longitud;

        public Location(int latitud, int longitud)
        {
            this.latitud = latitud;
            this.longitud = longitud;
        }

        public bool CheckSameLocation(Operator operatorTheOther)
        {
            return (latitud == operatorTheOther.location.latitud && longitud == operatorTheOther.location.longitud);
        }

        public override string ToString()
        {
            return $"latitud: {latitud} y longitud: {longitud}";
        }
    }
}