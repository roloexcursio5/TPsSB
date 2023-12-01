namespace TPI
{
    internal class CommandOperatorTransferBattery : ICommand
    {
        private Barrack barrack;
        public CommandOperatorTransferBattery(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.OperatorTransfer();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador para transferir baterìa a otro operador");
        }
    }
}
