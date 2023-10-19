using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static RollandiAlejandro.Cochera;



namespace RollandiAlejandro
{
    internal class TP2
    {
        //Variables
        static bool fin = false;
        static Cochera[] cocheraFinita = new Cochera[12];
        static List<Cochera> cocheraInfinita = new List<Cochera>();
        static string accionElegida;

        static void Main(string[] args)
        {
            Console.WriteLine("\nArranca el programa\n");
            CreaCocheraFinita();
            while (!fin)
            {
                MuestraMenu();
                accionElegida = Console.ReadLine();
                RealizaTareaSolicitada();
            }
        }

        static void CreaCocheraFinita()
        {
            for (int i = 0; i < cocheraFinita.Length; i++)
            {
                cocheraFinita[i] = new Cochera(i + 1);
            }
        }

        static void MuestraMenu()
        {
            Console.WriteLine($"\nMenu\n0 (estado cochera)\n1 (llega/n auto/s)\n2 (remover vehículo por matricula)\n3 (remover vehículo por dni)\n4 (remover vehículos por rango)\n5 (reordena y eficientiza cochera por tamaño)\n(cualquier otra letra para salir)");
        }

        static void RealizaTareaSolicitada()
        {
            if (accionElegida == "0")
                EstadoCochera();
            else if (accionElegida == "1")
                CreaAutoYLoEstaciona();
            else if (accionElegida == "2")
                RemueveVehiculoPorMatricula();
            else if (accionElegida == "3")
                RemueveVehiculoPorDNI();
            else if (accionElegida == "4")
                RemueveVehiculos();
            else if (accionElegida == "5")
                EficientizaLugares();
            else
                fin = true;
        }

        static void EstadoCochera()
        {
            Console.WriteLine($"BOX\tAnch-Larg\tTamaño\tVIP\tOcupado\tAUTO Anc-Lar\tTamaño\tVIP\tDNI\t\tModelo\tMatricula\tAuto=Cochera");
            for (int i = 0; i < cocheraFinita.Length; i++)
            {
                Console.WriteLine($"{i + 1}\t{(float)cocheraFinita[i].cocheraAncho / 100.00f}-{(float)cocheraFinita[i].cocheraLargo / 100.00}\t{cocheraFinita[i].cocheraTamaño}\t{cocheraFinita[i].cocheravip}\t{cocheraFinita[i].cocheraOcupada}\t{cocheraFinita[i].autoAncho}-{cocheraFinita[i].autoLargo}\t\t{cocheraFinita[i].autoTamaño}\t{cocheraFinita[i].dueñoVip}\t{cocheraFinita[i].dni}\t{cocheraFinita[i].modelo}\t{cocheraFinita[i].matricula}\t\t{cocheraFinita[i].coincideTamañoCocheraAuto}");
            }
            for (int i = 0; i < cocheraInfinita.Count; i++)
            {
                Console.WriteLine($"{i + 1 + cocheraFinita.Length}\t{(float)cocheraInfinita[i].cocheraAncho / 100.00f}-{(float)cocheraInfinita[i].cocheraLargo / 100.00}\t{cocheraInfinita[i].cocheraTamaño}\t{cocheraInfinita[i].cocheravip}\t{cocheraInfinita[i].cocheraOcupada}\t{cocheraInfinita[i].autoAncho}-{cocheraInfinita[i].autoLargo}\t\t{cocheraInfinita[i].autoTamaño}\t{cocheraInfinita[i].dueñoVip}\t{cocheraInfinita[i].dni}\t{cocheraInfinita[i].modelo}\t{cocheraInfinita[i].matricula}\t\t{cocheraInfinita[i].coincideTamañoCocheraAuto}");
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////// 
        /// /////////////////////////////////////////////////////////////////////////////////
        /// Acá es donde se crea y estacionan los autos
        private static void CreaAutoYLoEstaciona()
        {
            int autos = IngresoNumeroConControl("Cuántos autos llegan?");

            for (int i = 0; i < autos; i++)
            {
                Auto auto = CreaAuto();
                EstacionaAuto(auto);
            }
        }

        private static Auto CreaAuto()
        {
            Auto auto = new Auto();

            //salida de control
            //Console.WriteLine($"{(float)auto.autoAncho / 100.00f}-{(float)auto.autoLargo / 100.00}\t{auto.autoTamaño}\t{auto.dueñoVip}\t{auto.dni}\t{auto.modelo}\t{auto.matricula}");

            return auto;
        }

        private static void EstacionaAuto(Auto auto)
        {
            bool yaEstaciono = false;
            int i = 0;

            // recorre cochera fija
            while (!yaEstaciono && i < cocheraFinita.Length)
            {
                bool cocheraVacia = !cocheraFinita[i].cocheraOcupada;
                bool vip = ChequeaSiEsVip(auto, cocheraFinita[i]);
                bool tamaño = ChequeaSiEntraPorTamaño(auto, cocheraFinita[i]);

                if (cocheraVacia && vip && tamaño)
                {
                    OcupaCochera(auto, cocheraFinita[i]);
                    yaEstaciono = true;
                }
                i++;
            }

            // recorre cochera cuántica
            while (!yaEstaciono && i < (cocheraFinita.Length + cocheraInfinita.Count))
            {
                bool cocheraVacia = !cocheraInfinita[i - cocheraFinita.Length].cocheraOcupada;
                bool vip = ChequeaSiEsVip(auto, cocheraInfinita[i - cocheraFinita.Length]);
                bool tamaño = ChequeaSiEntraPorTamaño(auto, cocheraInfinita[i - cocheraFinita.Length]);

                if (cocheraVacia && vip && tamaño)
                {
                    OcupaCochera(auto, cocheraInfinita[i - cocheraFinita.Length]);
                    yaEstaciono = true;
                }
                i++;
            }

            // si sigue sin encontrar espacio crea aleatoriamente una cochera cuántica más, hasta que se cree una en la que por condiciones se pueda estacionar
            while (!yaEstaciono)
            {
                cocheraInfinita.Add(new Cochera(i));

                bool cocheraVacia = !cocheraInfinita[i - cocheraFinita.Length].cocheraOcupada;
                bool vip = ChequeaSiEsVip(auto, cocheraInfinita[i - cocheraFinita.Length]);
                bool tamaño = ChequeaSiEntraPorTamaño(auto, cocheraInfinita[i - cocheraFinita.Length]);

                if (cocheraVacia && vip && tamaño)
                {
                    OcupaCochera(auto, cocheraInfinita[i - cocheraFinita.Length]);
                    yaEstaciono = true;
                }
                i++;
            }
        }

        private static bool ChequeaSiEsVip(Auto auto, Cochera cochera)
        {
            bool isVip = false;
            if (auto.dueñoVip) // el dueño vip puede estacionar en lugar normal
                isVip = true;
            else if (cochera.cocheravip == auto.dueñoVip)
                isVip = true;
            else
                isVip = false;

            return isVip;
        }

        private static bool ChequeaSiEntraPorTamaño(Auto auto, Cochera cochera)
        {
            bool entraPorTamaño = false;
            if (cochera.cocheraTamaño == TamañoCocheraAuto.max.ToString()) // si la cochera es grande entra cualquier auto
                entraPorTamaño = true;
            else if (auto.autoTamaño == TamañoCocheraAuto.mini.ToString()) // si el auto es chico entra en cualquier cochera
                entraPorTamaño = true;
            else if (cochera.cocheraTamaño == auto.autoTamaño)
                entraPorTamaño = true;
            else
                entraPorTamaño = false;

            return entraPorTamaño;
        }

        private static void OcupaCochera(Auto auto, Cochera cochera)
        {
            cochera.cocheraOcupada = true;
            cochera.autoAncho = auto.autoAncho;
            cochera.autoLargo = auto.autoLargo;
            cochera.autoTamaño = auto.autoTamaño;
            cochera.dueñoVip = auto.dueñoVip;
            cochera.dni = auto.dni;
            cochera.modelo = auto.modelo;
            cochera.matricula = auto.matricula;
            cochera.coincideTamañoCocheraAuto = cochera.cocheraTamaño == auto.autoTamaño ?true :false;
        }
        /// ///////////////////////////////////////////////////////////////////////////////// 
        /// /////////////////////////////////////////////////////////////////////////////////
        /// Acá estàn los distintos mètodos que desocupan cochera, ya sea por matricula, dni o rango
        private static void DesocupaCochera(Cochera cochera)
        {
            cochera.cocheraOcupada = false;
            cochera.autoAncho = 0;
            cochera.autoLargo = 0;
            cochera.autoTamaño = "";
            cochera.dueñoVip = false;
            cochera.dni = "";
            cochera.modelo = 0;
            cochera.matricula = "  \t";
            cochera.coincideTamañoCocheraAuto = false;
        }

        private static void RemueveVehiculoPorMatricula()
        {
            Console.WriteLine("Escriba matricula");
            string matriculaAEliminar = Console.ReadLine().Trim().ToUpper();

            bool seElimino = false;
            int i = 0;

            // recorre cochera fija
            while (!seElimino && i < cocheraFinita.Length)
            {
                if (cocheraFinita[i].matricula == matriculaAEliminar)
                {
                    DesocupaCochera(cocheraFinita[i]);
                    seElimino = true;
                    Console.WriteLine("Se eliminó vehìculo de la cochera finita");
                }
                i++;
            }

            // recorre cochera cuántica
            while (!seElimino && i < (cocheraFinita.Length + cocheraInfinita.Count))
            {
                if (cocheraInfinita[i - cocheraFinita.Length].matricula == matriculaAEliminar)
                {
                    DesocupaCochera(cocheraInfinita[i - cocheraFinita.Length]);
                    seElimino = true;
                    Console.WriteLine("Se eliminó vehìculo de la cochera infinita");
                }
                i++;
            }

        }

        private static void RemueveVehiculoPorDNI()
        {
            Console.WriteLine("Escriba dni");
            string dniAEliminar = Console.ReadLine().Trim().ToUpper();

            bool seElimino = false;
            int i = 0;

            // recorre cochera fija
            while (!seElimino && i < cocheraFinita.Length)
            {
                if (cocheraFinita[i].dni == dniAEliminar)
                {
                    DesocupaCochera(cocheraFinita[i]);
                    seElimino = true;
                    Console.WriteLine("Se eliminó vehìculo de la cochera finita");
                }
                i++;
            }

            // recorre cochera cuántica
            while (!seElimino && i < (cocheraFinita.Length + cocheraInfinita.Count))
            {
                if (cocheraInfinita[i - cocheraFinita.Length].dni == dniAEliminar)
                {
                    DesocupaCochera(cocheraInfinita[i - cocheraFinita.Length]);
                    seElimino = true;
                    Console.WriteLine("Se eliminó vehìculo de la cochera infinita");
                }
                i++;
            }

        }
 
        private static void RemueveVehiculos()
        {
            int i = IngresoNumeroConControl("Escriba cochera desde") -1;
            int hasta = i + IngresoNumeroConControl("Escriba cuántas cocheras a vaciar?");
            hasta = hasta >= (cocheraFinita.Length + cocheraInfinita.Count) ? (cocheraFinita.Length + cocheraInfinita.Count) : hasta; // controla que no se exceda de las cocheras existentes

            if (hasta < (cocheraFinita.Length))
            {
                while (i < hasta)
                {
                    DesocupaCochera(cocheraFinita[i]);
                    Console.WriteLine("Se eliminó vehìculo de la cochera finita");
                    i++;
                }
            }
            else
            {
                while (i < cocheraFinita.Length)
                {
                    DesocupaCochera(cocheraFinita[i]);
                    Console.WriteLine("Se eliminó vehìculo de la cochera finita");
                    i++;
                }
                while (i < hasta)
                {
                    DesocupaCochera(cocheraInfinita[i - cocheraFinita.Length]);
                    Console.WriteLine("Se eliminó vehìculo de la cochera infinita");
                    i++;
                }
            }
        }
        /// ///////////////////////////////////////////////////////////////////////////////// 
        /// /////////////////////////////////////////////////////////////////////////////////
        /// Acá están las funciones para eficientizar
        private static void EficientizaLugares() // es muy parecida a EstacionaAuto() pero más restrictiva, y vacìa la cochera que estaba ocupando, y elimina cocheras cuánticas vacìas
        {
           // eficientiza los autos ineficientes en la cochera fija
           for (int i = 0; i < cocheraFinita.Length; i++)
           {
                if (cocheraFinita[i].cocheraOcupada && !cocheraFinita[i].coincideTamañoCocheraAuto) //si està bien estacionado en la fija, se deja
                {
                    Auto auto = CreaAuto();
                    PisaAutoCreadoAleatorioPorElExistenteEnCochera(auto, cocheraFinita[i]);
                    EstacionaAutoRestrictivo(auto);
                    DesocupaCochera(cocheraFinita[i]);
                }
           }

           // eficientiza los autos ineficientes en la cochera cuántica
            for (int i = 0; i < cocheraInfinita.Count; i++)
            {
                if (cocheraInfinita[i].cocheraOcupada) // && !cocheraInfinita[i].coincideTamañoCocheraAuto
                {
                    Auto auto = CreaAuto();
                    PisaAutoCreadoAleatorioPorElExistenteEnCochera(auto,cocheraInfinita[i]);
                    EstacionaAutoRestrictivo(auto);
                    DesocupaCochera(cocheraInfinita[i]);
                }
            }

            // elimino cocheras cuánticas vacías
            for (int i = 0; i < cocheraInfinita.Count; i++)
            {
                 // para sumar los espacios que borra
                if (!cocheraInfinita[i].cocheraOcupada)
                {
                    cocheraInfinita.RemoveAt(i);
                    i--;
                }
            }
        }

        private static void PisaAutoCreadoAleatorioPorElExistenteEnCochera(Auto auto, Cochera cochera)
        {
            auto.autoAncho = cochera.autoAncho;
            auto.autoLargo = cochera.autoLargo;
            auto.autoTamaño = cochera.autoTamaño;
            auto.dueñoVip = cochera.dueñoVip;
            auto.dni = cochera.dni;
            auto.modelo = cochera.modelo;
            auto.matricula = cochera.matricula;

            // salida de control
            //Console.WriteLine($"{(float)auto.autoAncho / 100.00f}-{(float)auto.autoLargo / 100.00}\t{auto.autoTamaño}\t{auto.dueñoVip}\t{auto.dni}\t{auto.modelo}\t{auto.matricula}");
        }

        private static void EstacionaAutoRestrictivo(Auto auto)
        {
            bool yaEstaciono = false;
            int i = 0;

            // recorre cochera fija
            while (!yaEstaciono && i < cocheraFinita.Length)
            {
                bool cocheraVacia = !cocheraFinita[i].cocheraOcupada;
                bool vip = cocheraFinita[i].cocheravip == auto.dueñoVip ? true : false;
                bool tamaño = cocheraFinita[i].cocheraTamaño == auto.autoTamaño ? true : false;

                if (cocheraVacia && vip && tamaño)
                {
                    OcupaCochera(auto, cocheraFinita[i]);
                    yaEstaciono = true;
                }
                i++;
            }

            // recorre cochera cuántica
            while (!yaEstaciono && i < (cocheraFinita.Length + cocheraInfinita.Count))
            {
                bool cocheraVacia = !cocheraInfinita[i - cocheraFinita.Length].cocheraOcupada;
                bool vip = cocheraInfinita[i - cocheraFinita.Length].cocheravip == auto.dueñoVip ? true : false;
                bool tamaño = cocheraInfinita[i - cocheraFinita.Length].cocheraTamaño == auto.autoTamaño ? true : false;

                if (cocheraVacia && vip && tamaño)
                {
                    OcupaCochera(auto, cocheraInfinita[i - cocheraFinita.Length]);
                    yaEstaciono = true;
                }
                i++;
            }

            // si sigue sin encontrar espacio crea aleatoriamente una cochera cuántica más, hasta que se cree una en la que por condiciones se pueda estacionar
            while (!yaEstaciono)
            {
                cocheraInfinita.Add(new Cochera(i));

                bool cocheraVacia = !cocheraInfinita[i - cocheraFinita.Length].cocheraOcupada;
                bool vip = cocheraInfinita[i - cocheraFinita.Length].cocheravip == auto.dueñoVip ? true : false;
                bool tamaño = cocheraInfinita[i - cocheraFinita.Length].cocheraTamaño == auto.autoTamaño ? true : false;

                if (cocheraVacia && vip && tamaño)
                {
                    OcupaCochera(auto, cocheraInfinita[i - cocheraFinita.Length]);
                    yaEstaciono = true;
                }
                i++;
            }
        }

        /// ///////////////////////////////////////////////////////////////////////////////// <summary>
        /// /////////////////////////////////////////////////////////////////////////////////
        /// función comun
        private static int IngresoNumeroConControl(string mensaje)
        {
            int valorNumerico = 0;
            bool numeroValido = false;

            Console.WriteLine(mensaje);
            numeroValido = int.TryParse(Console.ReadLine().Trim(), out valorNumerico);

            while (!numeroValido)
            {
                Console.WriteLine("Número no válido - " + mensaje);
                numeroValido = int.TryParse(Console.ReadLine().Trim(), out valorNumerico);
            }
            return valorNumerico;
        }
    }
}
