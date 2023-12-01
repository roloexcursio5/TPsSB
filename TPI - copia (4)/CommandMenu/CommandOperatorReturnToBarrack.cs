namespace TPI
{
    internal class CommandOperatorReturnToBarrack : ICommand
    {   
        private Barrack barrack;
        public CommandOperatorReturnToBarrack(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.OperatorReturnToBarrack();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Seleccionar un operador específico para retornar al cuartel");
        }
    }
}
