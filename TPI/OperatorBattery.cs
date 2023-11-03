using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPI
{
    internal class OperatorBattery
    {
        public int actual;
        public int maximum;

        public OperatorBattery(int actual, int maximum)
        {
            this.actual = actual;
            this.maximum = maximum;
        }

        public int BatteryToGive()
        {
            return actual - (int)(maximum * 0.10f);
        }
        public int BatteryToReceive()
        {
            return maximum - actual;
        }

        public string CheckBatteryToGive()
        {
            string message = (BatteryToGive() <= 0) ? "El operador se quedó con la reserva, no tiene batería que prestar\n" : "";

            return message;
        }

        public string  CheckBatteryToReceive()
        {
            string message = (BatteryToReceive() == 0) ? "El operador receptor tiene la batería llena\n" :"";

            return message;
        }
    }
}
