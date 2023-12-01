namespace TPI
{
    internal class CommandOperatorStatusAvailable : ICommand
    {
        private Barrack barrack;
        public CommandOperatorStatusAvailable(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.SetOperatorStatus(EnumOperatorStatus.available);
        }

        public void ReportPurpose()
        {
            Console.WriteLine(@"Cambiar estado del operador a ""AVAILABLE""");
        }
    }
}