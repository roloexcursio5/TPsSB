namespace TPI
{
    internal abstract class OperatorElement
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        public OperatorElement(Operator o, int oUAV, int oK9, int oM8)
        {
            ElementDefinition(o, oUAV, oK9, oM8);  
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

        public void FullCapacity()
        {
            Actual = Maximum;
            Console.WriteLine($"\n{GetType().ToString().Split(".")[1]} completa");
        }
        public int ElementToGive()
        {
            int elementToGiveAmount = 0;

            if (this is OperatorBattery)
                elementToGiveAmount = Actual - (int)(Maximum * 0.10f);
            if (this is OperatorLoad)
                elementToGiveAmount = Actual;

            return elementToGiveAmount;
        }
        public int ElementToReceive()
        {
            return Maximum - Actual;
        }

        public void ElementTransfered(int elementTransferAmount)
        {
            Actual -= elementTransferAmount;
        }
        public void ElementReceived(int elementTransferAmount)
        {
            Actual += elementTransferAmount;
        }

        public override string ToString()
        {
            return $"{GetType().ToString().Split(".")[1]}: actual {Actual} - máxima {Maximum}";
        }
    }
}
