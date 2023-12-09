namespace TPI
{
    internal class OperatorLoad : OperatorElement
    {
        public OperatorLoad(Operator o) : base(o, 5, 40, 250)
        {
        }
        public override void ElementActualValueDefinition()
        {
            Actual = 2;   // para probar cosas
        }
        public int LoadSpaceUsed()
        {
            return Actual / Maximum * 100;
        }

        public bool LoadEntryValidation(out int load)
        {
            string message = "";

            if (!int.TryParse(CommonFunctions.UserInput("Ingrese la cantidad de Kilos a cargar"), out load))
                message += "Debes cargar un número\n";
            if (load < 1)
                message += "Los kilos tienen que ser un número positivo mayor que cero\n";
            if (load > ElementToReceive())
                message += $"Tu peso actual es de {Actual} kilo y quieres cargar {load} kilos por lo cual excedes la capacidad máxima que es de {Maximum} kilos\n";

            CommonFunctions.IfErrorMessageShowIt(message);

            return message == "";
        }

        public void UnLoad()
        {
            Actual = 0;
        }
    }
}
