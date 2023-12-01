namespace TPI
{
    internal class CommandOperatorTransferLoad : ICommand
    {
        private Barrack barrack;
        public CommandOperatorTransferLoad(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.OperatorTransferLoad();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador específico para cargar");
        }
    }
}
