namespace TPI
{
    internal class OperatorM8 : Operator //semi-humanoides de carga
    {
        public OperatorM8(int IDautoincremental, Barracks barracks)
        {
            uniqueID = IDautoincremental;
            generalStatus = OperatorStatus.active;
            battery = new OperatorBattery(2000, 12250);  //mAh miliAmperios, 1000mAh = 1 hour use
            load = new OperatorLoad(50, 250); // kilos
            speed = new OperatorSpeed(30,30); // kilometros/hora
            location = new Location(barracks.location.latitud, barracks.location.longitud); // actual
        }
    }
}
