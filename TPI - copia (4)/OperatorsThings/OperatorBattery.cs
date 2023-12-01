using System.Xml;

namespace TPI
{
    internal class OperatorBattery : OperatorElement
    {
        public OperatorBattery(Operator o) : base(o, 4000, 6500, 12250)
        {
        }
        public override void ElementActualValueDefinition()
        {
            Actual = Maximum/2;
        }

        public void BatteryRecharge()
        {
            Actual = Maximum;
            Console.WriteLine("Baterìa al 100%");
        }
        public int BatteryToGive()
        {
            return Actual - (int)(Maximum * 0.10f);
        }
        public string BatteryToGiveErrorMessage()
        {
            return "ponele";
        }
        public int BatteryToReceive()
        {
            return Maximum - Actual;
        }

        public int DistanceBatteryAutonomy(OperatorSpeed speed)
        {
            return Actual / 1000 * speed.Actual;
        }
    }
}
