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
            SaveLoadFunctions.ExitMatch(barrack);
        }

        public void ReportPurpose()
        {
            Console.WriteLine("para salir");
        }
    }
}
