namespace TPI
{
    internal class CommandQuery : ICommand
    {
        public void Execute()
        {
            /// a mejorar esto es una prueba ver de traer datos de SQL (incluso si dichos datos no salen del juego sino de una carga manual en SQL   FALTA
            DatabaseQuerys.GetAllData();
            DatabaseQuerys.KilometersTotal();
            DatabaseQuerys.KilometersOperator(0);
        }

        public void ReportPurpose()
        {
            Console.WriteLine("Prueba la consulta de datos a una base");
        }
    }
}
