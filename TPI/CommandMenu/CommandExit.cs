namespace TPI
{
    internal class CommandExit : ICommand
    {
        private Barrack barrack;
        public CommandExit(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            CommonFunctions.ExitMatch(barrack);
        }

        public void ReportPurpose()
        {
            Console.WriteLine("para salir");
        }
    }
}
