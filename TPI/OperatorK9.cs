namespace TPI
{
    internal class OperatorK9 : Operator // unidades cuadrúpedas
    {
        public OperatorK9(int IDautoincremental, Barrack barracks)
        {
            UniqueID = IDautoincremental;
            GeneralStatus = OperatorStatus.active;
            Battery = new OperatorBattery(6500);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(40); // kilos
            Speed = new OperatorSpeed(40); // kilometros/hora
            Location = new Location(barracks.Location.Latitud,barracks.Location.Longitud); // actual
        }
    }
}
