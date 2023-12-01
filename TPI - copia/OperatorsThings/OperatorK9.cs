namespace TPI
{
    internal class OperatorK9 : Operator // unidades cuadrúpedas
    {
        public OperatorK9(int IDautoincremental, Barrack barracks)
        {
            UniqueID = IDautoincremental;
            GeneralStatus = EnumOperatorStatus.available;
            Battery = new OperatorBattery(this);  //mAh miliAmperios, 1000mAh = 1 hour use
            Load = new OperatorLoad(this); // kilos
            Speed = new OperatorSpeed(40); // kilometros/hora
            Location = new Location(barracks.Location.Latitud,barracks.Location.Longitud); // actual
    }
    }
}
