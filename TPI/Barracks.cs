namespace TPI
{
    internal class Barracks
    {
        List<Operator> operators = new List<Operator>();

        // para crear aleatoriamente 10 operadores
        public Barracks()
        {
            Random random = new Random();

            for(int i = 0; i < 10; i++)
            {
                int operatorType = random.Next(3);
                if (operatorType == 0)
                    operators.Add(new OperatorUAV());
                if (operatorType == 1)
                    operators.Add(new OperatorK9());
                if (operatorType == 2)
                    operators.Add(new OperatorM8());
            }
        }

        public void ShowOperatos()
        {
            Console.WriteLine("\nOperadores:");
            for (int i = 0; i < operators.Count; i++)
            {
                Console.WriteLine($"{i} - {operators[i]} - {operators[i].generalStatus}");
            }
            Console.WriteLine("\n\n");
        }

        //se opera desde el cuartel que tiene estado y asigna òrdenes

        // del cuartel podemos ralizar varias operacione en particular
        // listar el estado de todos los operadores
        // listar el estado de todos los operadores que estèn en cierta localizaciòn
        // hacer un total recall (llamado y retorno) general a todos los peradores
        // seleccionar un oerpador en especìfico y: enviarlo a una localizaciòn en especial, indicar retorno a cuartel, ccambiar estado a STANDBY una entidad en STANF BU no puede ser utilizada por comandos genreales
        // agregar o remover operadores de la reserva
    }
}
