
namespace TPI
{
    internal class OperatorBattery
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        public OperatorBattery(Operator o)
        {
            BatteryDefinition(o);
            Actual = Maximum;
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
