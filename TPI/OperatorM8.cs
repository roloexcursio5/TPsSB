namespace TPI
{
    internal class OperatorM8 : Operator //semi-humanoides de carga
    {
        public OperatorM8()
        {
            uniqueID = UniqueID();
            battery = new OperatorBattery(2000, 12250);  //mAh miliAmperios, 1000mAh = 1 hour use
            generalStatus = OperatorStatus.active;
            load = new OperatorLoad(50, 250); // kilos
            optimalSpeed = 30; // kilometros/hora
            location = ""; // actual
        }
    }
}
