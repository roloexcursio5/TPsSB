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
            barrack.OperatorLoadTransfer();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador para transferir su carga a otro operador");
        }
    }
}
