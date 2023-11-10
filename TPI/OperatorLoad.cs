namespace TPI
{
    internal class OperatorLoad
    {
        public int actual;
        public int maximum;

        public OperatorLoad(int actual, int maximum)
        {
            this.actual = actual;
            this.maximum = maximum;
        }

        public int LoadToGive()
        {
            return actual;
        }
        public int LoadToReceive()
        {
            return maximum - actual;
        }

        protected int LoadPercentage()
        {
            return actual / maximum * 100;
        }
    }
}
