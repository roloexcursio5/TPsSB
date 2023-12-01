namespace TPI
{
    internal class OperatorLoad : OperatorElement
    {
        public OperatorLoad(Operator o) : base(o, 5, 40, 250)
        {
        }
        public override void ElementActualValueDefinition()
        {
            Actual = 0;
        }

        public int LoadToGive()
        {
            return Actual;
        }
        public int LoadToReceive()
        {
            return Maximum - Actual;
        }

        //protected int LoadPercentage()
        //{
        //    return Actual / Maximum * 100;
        //}
        public void UnLoade()
        {
            Actual = 0;
        }

        public override string ToString()
        {
            return $"Carga: actual {Actual} - máxima {Maximum}";
        }
    }
}
