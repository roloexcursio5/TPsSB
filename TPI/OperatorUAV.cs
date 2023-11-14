namespace TPI
{
    internal class OperatorUAV : Operator//drones voladores de varias hélices
    {
        public OperatorUAV(int IDautoincremental, Barrack barracks)
        {
            UniqueID = IDautoincremental;
            GeneralStatus = OperatorStatus.active;
            Battery = new OperatorBattery(4000);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(5); // kilos
            Speed = new OperatorSpeed(50); // kilometros/hora
            Location = new Location(barracks.Location.Latitud, barracks.Location.Longitud);
        }
    }
}
