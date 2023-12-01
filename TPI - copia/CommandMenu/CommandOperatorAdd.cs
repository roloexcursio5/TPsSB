namespace TPI
{
    internal class CommandOperatorAdd : ICommand
    {
        private Barrack barrack;
        public CommandOperatorAdd(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.CreateAnOperatorRandomly(1);
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Agregar un operador a la Reserva");
        }
    }
}
