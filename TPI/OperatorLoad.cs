using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPI
{
    internal class OperatorLoad
    {
        public int actual;
        public int maximum;

        public OperatorLoad(int actual, int maximum)
        {
            this.actual = actual;
            this.maximum = maximum;
        }

        public int LoadToGive()
        {
            return actual;
        }
        public int LoadToReceive()
        {
            return maximum - actual;
        }

        public string CheckLoadToGive()
        {
            string message = (LoadToGive() <= 0) ? "El operador no tiene carga que transferir\n" : "";

            return message;
        }

        public string CheckLoadToReceive()
        {
            string message = (LoadToReceive() == 0) ? "El operador receptor tiene la capacidad de carga llena\n" : "";

            return message;
        }
    }
}
