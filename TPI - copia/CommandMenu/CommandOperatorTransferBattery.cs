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
            barrack.OperatorTransferBattery();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador específico para cargar");
        }
    }
}
