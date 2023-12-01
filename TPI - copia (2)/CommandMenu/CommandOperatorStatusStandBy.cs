namespace TPI
{
    internal class CommandOperatorStatusStandBy : ICommand
    {
        private Barrack barrack;
        public CommandOperatorStatusStandBy(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.SetOperatorStatus(EnumOperatorStatus.standBy);
        }

        public void ReportPurpose()
        {
            Console.WriteLine(@"Cambiar estado del operador a ""STANDBY""");
        }
    }
}