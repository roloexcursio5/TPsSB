namespace TPI
{
    internal class CommandTotalRecall : ICommand
    {
        private Barrack barrack;
        public CommandTotalRecall(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.TotalRecall();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Llamado y retorno de todos los operadores (Total recall)");
        }
    }
}
