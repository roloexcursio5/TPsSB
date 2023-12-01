using System.Net.Http.Headers;

namespace TPI
{
    internal class CommandMenu
    {
        List<ICommand> commands;
        private static CommandMenu _instance;
        public static CommandMenu GetInstance(Barrack barrack)
        {
            if (_instance == null)
            {
                _instance = new CommandMenu(barrack);
            }
            return _instance;
        }

        private CommandMenu(Barrack barrack)
        {
            commands = new List<ICommand>()
            {
                new CommandShowOperators(barrack),
                new CommandShowOperatorsInLocation(barrack),
                new CommandTotalRecall(barrack),
                new CommandTotalRecallFix(barrack),
                new CommandTotalBatteryRecharge(barrack),
                new CommandOperatorSendLocation(barrack),
                new CommandOperatorLoad(barrack),
                new CommandOperatorReturnToBarrack(barrack),
                new CommandOperatorAdd(barrack),
                new CommandOperatorRemove(barrack),
                new CommandOperatorStatusStandBy(barrack),
                new CommandOperatorStatusAvailable(barrack),
                new CommandOperatorTransferBattery(barrack),
                new CommandOperatorTransferLoad(barrack),
                new CommandExit(barrack)
            };
    }

        public void DisplayMenu()
        {
            Console.WriteLine("\nSeleccione una opción:");
            foreach (ICommand command in commands)
            {
                Console.Write(commands.IndexOf(command) + " - ");
                command.ReportPurpose();
            }
            Console.WriteLine("\nSu eleccion:");
            ExecuteCommand(Console.ReadLine().Trim().ToLower());
        }

        public void ExecuteCommand(string selectedOption)
        {
            bool esNumero = int.TryParse(selectedOption, out int option);
            bool esValido = option < commands.Count;

            if (esNumero && esValido)
            {
                commands[option].Execute();
                DisplayMenu();
            }
            else
            {
                Console.WriteLine("No has seleccionado ninguna opción válida, por lo tanto vuelves al menú principal\n\n");
                DisplayMenu();
            }
        }
    }
}


