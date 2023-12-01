namespace TPI
{
    internal class OperatorM8 : Operator //semi-humanoides de carga
    {
        public OperatorM8(int IDautoincremental, Barrack barracks)
        {
            UniqueID = IDautoincremental;
            GeneralStatus = EnumOperatorStatus.available;
            Battery = new OperatorBattery(this);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(this); // kilos
            Speed = new OperatorSpeed(30); // kilometros/hora
            Location = new Location(barracks.Location.Latitud, barracks.Location.Longitud); // actual
        }
    }
}
