namespace TPI
{
    internal class OperatorUAV : Operator//drones voladores de varias hélices
    {
        public OperatorUAV(int IDautoincremental, Barrack barracks)
        {
            UniqueID = IDautoincremental;
            GeneralStatus = EnumOperatorStatus.available;
            Battery = new OperatorBattery(this);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(this); // kilos
            Speed = new OperatorSpeed(50); // kilometros/hora
            Location = new Location(barracks.Location.Latitud, barracks.Location.Longitud);
        }
    }
}
