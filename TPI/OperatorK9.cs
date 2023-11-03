namespace TPI
{
    internal class OperatorK9 : Operator // unidades cuadrúpedas
    {
        public OperatorK9()
        {
            uniqueID = UniqueID();
            battery = new OperatorBattery(2000,6500);  //mAh miliAmperios, 1000mAh = 1 hour use
            generalStatus = OperatorStatus.active;
            load = new OperatorLoad(5,40); // kilos
            optimalSpeed = 40; // kilometros/hora
            location = ""; // actual
        }
    }
}
