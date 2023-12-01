using System.Xml;

namespace TPI
{
    internal class OperatorBattery2 : OperatorElement
    {
        public OperatorBattery2(Operator o) : base(o, 4000, 6500, 12250)
        {
        }
        public override void ElementActualValueDefinition()
        {
            Actual = Maximum/2;
        }

        private void BatteryDefinition(Operator o)
        {
            if (o is OperatorUAV)
                Maximum = 4000;
            if (o is OperatorK9)
                Maximum = 6500;
            if (o is OperatorM8)
                Maximum = 12250;
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

        public override string ToString()
        {
            return $"Bateria actual: {Actual} - Baterìa máxima: {Maximum}";
        }
    }
}
