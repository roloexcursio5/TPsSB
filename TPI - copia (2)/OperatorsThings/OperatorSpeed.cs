namespace TPI
{
    internal class OperatorSpeed
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        public OperatorSpeed(int maximum)
        {
            Actual = maximum;
            Maximum = maximum;
        }

        public void ActualSpeedAdjustedByLoad(OperatorLoad load)
        {
            int loadPercentage = load.Actual / load.Maximum * 100;
            Actual = (int)(Maximum * 0.05f * (loadPercentage / 10));
        }
    }
}
