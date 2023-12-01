namespace TPI
{
    internal class CommandExit : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Has salido del programa");
            Environment.Exit(0);
        }

        public void ReportPurpose()
        {
            Console.WriteLine("para salir");
        }
    }
}
