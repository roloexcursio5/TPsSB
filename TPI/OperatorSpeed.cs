namespace TPI
{
    internal class OperatorSpeed
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        //public int actual;
        //public int maximum;

        public OperatorSpeed(int maximum)
        {
            this.Actual = maximum;
            this.Maximum = maximum;
        }

        protected void ActualSpeedAdjustedByLoad(int loadPercentage)
        {
            Actual = (int)(Maximum * 0.05f * (loadPercentage / 10));
        }
    }
}
