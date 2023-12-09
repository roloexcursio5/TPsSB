namespace TPI
{
    internal class CommandOperatorRemove : ICommand
    {
        private Barrack barrack;
        public CommandOperatorRemove(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.RemoveOperator();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Remover operador de la Reserva");
        }
    }
}
