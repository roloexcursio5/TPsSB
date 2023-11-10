namespace TPI
{
    internal class OperatorUAV : Operator//drones voladores de varias hélices
    {
        public OperatorUAV(int IDautoincremental, Barracks barracks)
        {
            uniqueID = IDautoincremental;
            generalStatus = OperatorStatus.active;
            battery = new OperatorBattery(2000, 4000);  //mAh miliAmperios, 1000mAh = 1 hour use
            load = new OperatorLoad(5, 5); // kilos
            speed = new OperatorSpeed(50,50); // kilometros/hora
            location = new Location(barracks.location.latitud, barracks.location.longitud);
        }
    }
}
