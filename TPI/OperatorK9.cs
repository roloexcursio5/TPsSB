namespace TPI
{
    internal class OperatorK9 : Operator // unidades cuadrúpedas
    {
        public OperatorK9(int IDautoincremental, Barracks barracks)
        {
            uniqueID = IDautoincremental;
            generalStatus = OperatorStatus.active;
            battery = new OperatorBattery(2000,6500);  //mAh miliAmperios, 1000mAh = 1 hour use
            load = new OperatorLoad(5,40); // kilos
            speed = new OperatorSpeed(40,40); // kilometros/hora
            location = new Location(barracks.location.latitud,barracks.location.longitud); // actual
        }
    }
}
