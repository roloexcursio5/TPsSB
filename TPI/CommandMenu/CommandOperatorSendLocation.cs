namespace TPI
{
    internal class CommandOperatorSendLocation : ICommand
    {
        private Barrack barrack;
        public CommandOperatorSendLocation(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.OperatorSendLocation();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador específico para enviar a una nueva localización");
        }
    }
}
