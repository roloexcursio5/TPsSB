namespace TPI
{
    internal class CommandOperatorLoad : ICommand
    {
        private Barrack barrack;
        public CommandOperatorLoad(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.OperatorLoad();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador específico para cargar");
        }
    }
}
