namespace TPI
{
    internal class OperatorSpeed
    {
        public int actual;
        public int maximum;

        public OperatorSpeed(int actual, int maximum)
        {
            this.actual = actual;
            this.maximum = maximum;
        }

        protected void ActualSpeedAdjustedByLoad(int loadPercentage)
        {
            actual = (int)(maximum * 0.05f * (loadPercentage / 10));

        }
    }
}
