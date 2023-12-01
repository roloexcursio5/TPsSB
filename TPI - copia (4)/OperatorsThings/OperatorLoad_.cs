namespace TPI
{
    internal class OperatorLoad_
    {
        public int Actual { get; set; } = 0;
        public int Maximum { get; set; }

        public OperatorLoad_(Operator o)
        {
            LoadDefinition(o);
        }

        private void LoadDefinition(Operator o)
        {
            if (o is OperatorUAV)
                Maximum = 5;
            if (o is OperatorK9)
                Maximum = 40;
            if (o is OperatorM8)
                Maximum = 250;
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
