namespace TPI
{
    internal class CommandTotalRecallFix : ICommand
    {
        private Barrack barrack;
        public CommandTotalRecallFix(Barrack barrack)
        {
            this.barrack = barrack;
        }
        public void Execute()
        {
            barrack.TotalRecallFix();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Llamado y retorno de todos los operadores que estèn dañados para que se les haga mantenimiento");
        }
    }
}
