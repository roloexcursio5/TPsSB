using System.Xml;

namespace TPI
{
    internal abstract class OperatorElement
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        public OperatorElement(Operator o, int oUAV, int oK9, int oM8)
        {
            ElementDefinition(o, oUAV, oK9, oM8);  //4000, 6500, 12250
            ElementActualValueDefinition();
        }

        private void ElementDefinition(Operator o, int oUAV, int oK9, int oM8)
        {
            if (o is OperatorUAV)
                Maximum = oUAV;
            if (o is OperatorK9)
                Maximum = oK9;
            if (o is OperatorM8)
                Maximum = oM8;
        }
        public virtual void ElementActualValueDefinition() { }

        public void BatteryRecharge()
        {
            Actual = Maximum;
            Console.WriteLine("Baterìa al 100%");
        }
        public int ElementToGive()
        {
            int elementToGiveAmount = 0;

            if (this is OperatorBattery2)
                elementToGiveAmount = Actual - (int)(Maximum * 0.10f);
            if (this is OperatorLoad)
                elementToGiveAmount = Actual;

            return elementToGiveAmount;
        }
        public int ElementToReceive()
        {
            return Maximum - Actual;
        }

        public int DistanceBatteryAutonomy(OperatorSpeed speed)
        {
            return Actual / 1000 * speed.Actual;
        }

        public override string ToString()
        {
            return $"{GetType()} actual: {Actual} - {GetType()} máxima: {Maximum}";
        }
    }
}
