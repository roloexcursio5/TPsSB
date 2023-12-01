namespace TPI
{
    static internal class Messages
    {
        static public string NotEnoghBatteryToGiveMessage = "El operador se quedó con la reserva, no tiene batería que prestar\n";
        static public string NotEnoughElementToGiveMessage(OperatorElement operatorElement)
        {
            string message = "";
            if (operatorElement is OperatorBattery2)
                message += "El operador se quedó con la reserva, no tiene batería que prestar\n";
            if (operatorElement is OperatorLoad)
                message += "algo con operador\n";

            return message;
        }

        static public string ElementToReceiveFullMessage(OperatorElement operatorElement)
        {
            string message = "";
            if (operatorElement is OperatorBattery2)
                message += "El operador receptor tiene la batería llena\n";
            if (operatorElement is OperatorLoad)
                message += "algo con operador pero de carga\n";

            return message;
        }

        static public string ElementToTransferImposibilityMessage(OperatorElement operatorElement)
        {
            string message = "";
            if (operatorElement is OperatorBattery2)
                message += "No se pudo transferir la batería\n";
            if (operatorElement is OperatorLoad)
                message += "No se pudo transferir la carga\n";

            return message;
        }
    }
}
