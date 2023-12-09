namespace TPI
{
    internal class CommandMapPrint : ICommand
    {
        public void Execute()
        {
            Map.MapPrint();
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Imprime el mapa usando la primera letra del tipo de terreno (para màs detalle ver EnumLandType");
        }
    }
}
