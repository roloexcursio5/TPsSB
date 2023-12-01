using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
