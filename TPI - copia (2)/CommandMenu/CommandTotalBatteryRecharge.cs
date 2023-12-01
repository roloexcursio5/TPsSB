namespace TPI
{
    internal class CommandTotalBatteryRecharge : ICommand
    {
        private Barrack barrack;
        public CommandTotalBatteryRecharge(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.TotalBatteryRecharge();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Recargar la batería de todos los operadores que estén en un lugar que exista la posibilidad de reacargar");
        }
    }
}
