namespace TPI
{
    internal class OperatorUAV : Operator//drones voladores de varias hélices
    {
        public OperatorUAV()
        {
            uniqueID = UniqueID();
            battery = new OperatorBattery(2000, 4000);  //mAh miliAmperios, 1000mAh = 1 hour use
            generalStatus = OperatorStatus.active;
            load = new OperatorLoad(5, 5); // kilos
            optimalSpeed = 50; // kilometros/hora
            location = ""; // actual
        }
    }
}
