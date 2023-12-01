namespace TPI
{
    internal class CommandShowOperators : ICommand
    {
        private Barrack barrack;
        public CommandShowOperators(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.ShowOperators();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Listar el estado de todos los operadores");
        }
    }
}
