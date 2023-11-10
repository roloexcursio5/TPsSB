namespace TPI
{
    internal class OperatorBattery
    {
        public int actual;
        public int maximum;

        public OperatorBattery(int actual, int maximum)
        {
            this.actual = actual;
            this.maximum = maximum;
        }

        public int BatteryToGive()
        {
            return actual - (int)(maximum * 0.10f);
        }
        public int BatteryToReceive()
        {
            return maximum - actual;
        }
    }
}
