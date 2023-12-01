namespace TPI
{
    internal class CommandShowOperatorsInLocation : ICommand
    {
        private Barrack barrack;
        public CommandShowOperatorsInLocation(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.ShowOperatorsInLocation();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Listar el estado de todos los operadores en un lugar");
        }
    }
}
