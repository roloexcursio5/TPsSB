namespace TPI
{
    internal class OperatorBattery
    {
        public int Actual { get; set; }
        public int Maximum { get; set; }

        //public int actual;
        //public int maximum;

        public OperatorBattery(int maximum)
        {
            this.Actual = maximum;
            this.Maximum = maximum;
        }

        public int BatteryToGive()
        {
            return Actual - (int)(Maximum * 0.10f);
        }
        public int BatteryToReceive()
        {
            return Maximum - Actual;
        }
    }
}
