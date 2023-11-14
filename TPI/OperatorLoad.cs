namespace TPI
{
    internal class OperatorLoad
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        //public int actual;
        //public int maximum;

        public OperatorLoad(int maximum)
        {
            this.Actual = 0;
            this.Maximum = maximum;
        }

        public int LoadToGive()
        {
            return Actual;
        }
        public int LoadToReceive()
        {
            return Maximum - Actual;
        }

        protected int LoadPercentage()
        {
            return Actual / Maximum * 100;
        }
    }
}
