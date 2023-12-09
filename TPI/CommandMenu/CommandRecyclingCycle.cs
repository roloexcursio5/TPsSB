namespace TPI
{
    internal class CommandRecyclingCycle : ICommand
    {
        private Barrack barrack;
        public CommandRecyclingCycle(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.TotalrecyclingCycle();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Llamado a todos los operadores para que busquen el vertedero más cercano y luego vayan al lugar de reciclaje más cercano");
        }
    }
}
