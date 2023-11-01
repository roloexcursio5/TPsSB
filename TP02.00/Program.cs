using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using static RollandiAlejandro.Sucursal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RollandiAlejandro
{
    internal class Program
    {
        static List<Sucursal> sucursales = new List<Sucursal>();
        static bool saleDePrograma = false;
        static bool saleDeMenuSucursal = true;
        static string opcionElegida = "";
        static Sucursal sucursal = new Sucursal();

        static void Main(string[] args)
        {
            int cantidadSucursales = Convert.ToInt32(SolicitaInputYControlaSeaValido("Arranca el programa\nIngrese la cantidad de sucursales que desee se creen automáticamente", 100000));
            CreaSucursal(cantidadSucursales);

            while (!saleDePrograma)
            {
                MuestraSucursales();
                opcionElegida = SolicitaInputYControlaSeaValido("Elija el número de sucursal o s (para salir)",cantidadSucursales);
                RealizaAccionMenuPrincipal(opcionElegida);

                // entra en una sucursal
                while (!saleDeMenuSucursal)
                {
                    ImprimeVacunas();
                    ImprimeVirus();
                    MuestraMenuSucursal();
                    opcionElegida = SolicitaInputYControlaSeaValido("Elija el número de la tarea o s (para salir)", 5);
                    RealizaAccionMenuSucursal(opcionElegida);
                }
            }
        }

         static void CreaSucursal(int cantidadSucursales)
        {
            for (int i = 0; i < cantidadSucursales; i++)
            {
                sucursales.Add(new Sucursal());
            }
            Console.WriteLine();
        }

        static void MuestraSucursales()
        {
            for (int i = 0; i < sucursales.Count; i++)
            {
                Console.WriteLine($"{i} - {sucursales[i].ToString()}");
            }
        }

        private static void RealizaAccionMenuPrincipal(string opcionElegida)
        {
            if (opcionElegida == "S")
                saleDePrograma = true;
            else
            { 
                saleDeMenuSucursal = false;
                sucursal = sucursales[Convert.ToInt32(opcionElegida)];
                ProponeCrearVacunasYVirusAutomaticamente();
            }
        }

        static void ProponeCrearVacunasYVirusAutomaticamente()
        {
            Console.WriteLine("\nSi desea generar vacunas y virus automáticamente presionar s");
            if(Console.ReadLine().Trim().ToLower() == "s")
            {
                CreaVacunasAutomaticamente();
                CreaVirusAutomaticamente();
            }
        }

        private static void CreaVacunasAutomaticamente()
        {
            int cantidadVacunas = Convert.ToInt32(SolicitaInputYControlaSeaValido("Ingrese la cantidad de vacunas a crear", 100000000));

            for (int i = 0; i < cantidadVacunas; i++)
            {
                sucursal.vacunas.Add(new Vacuna(10, 3));
            }
        }

        private static void CreaVirusAutomaticamente()
        {
            int cantidadVirus = Convert.ToInt32(SolicitaInputYControlaSeaValido("Ingrese la cantidad de virus a crear", 100000000));

            for (int i = 0; i < cantidadVirus; i++)
            {
                sucursal.viruses.Add(new Virus(10, 4));
            }
        }

        private static void MuestraMenuSucursal()
        {
            Console.WriteLine($"\n"+@"Seleccione Opción:
                        0- Catalogar nueva vacuna
                        1- Sintetizar virus
                        2- Destruir vacuna o virus
                        3- Destruir por tipo
                        4- Activar sistema de autodestruccion sucursal
                        5- Activar sistema de autodestruccion global
                        o s (para salir)");
            
        }


        static private void RealizaAccionMenuSucursal(string input)
        {
            switch (input)
            {
                case "0":
                    CatalogaNuevaVacuna(sucursal.vacunas);
                    break;
                case "1":
                    SintetizaNuevoVirus(sucursal.vacunas, sucursal.viruses);
                    break;
                case "2":
                    DestruyeVacunaOVirus();
                    break;
                case "3":
                    Console.WriteLine("ponele");
                    break;
                case "4":
                    Console.WriteLine("ponele");
                    break;
                case "5":
                    Console.WriteLine("ponele");
                    break;
                case "6":
                    Console.WriteLine("ponele");
                    break;
                case "S":
                    saleDeMenuSucursal = true;
                    break;
            }
        }


        static void CatalogaNuevaVacuna(List<Vacuna> vacunas)
        {
            Vacuna vacuna = new Vacuna(vacunas);
            sucursal.vacunas.Add(vacuna);
            Console.WriteLine("\nCargaste exitosamente una nueva vacuna");
        }

        static void SintetizaNuevoVirus(List<Vacuna> vacunas, List<Virus> viruses)
        {
            if (BuscaVacunaActiva(vacunas))
            {
                Vacuna vacunaUltima = new Vacuna();
                vacunaUltima = vacunas[PosicionUltimaVacunaActiva(vacunas)];

                Console.WriteLine("Como existe al menos una vacuna sus valores sirven de referencia en el virus\nDatos vacuna: " + vacunaUltima.ToString());
                Virus virus = new Virus(viruses, vacunaUltima.codigo, vacunaUltima.tipo);
                sucursal.viruses.Add(virus);
                Console.WriteLine("Cargaste exitosamente un nuevo virus");
            }
            else
            {
                Console.WriteLine("No hay vacunas cargadas para usar como referencia por ende debe cargas todos los datos del virus usted");
                Virus virus = new Virus(viruses);  
                sucursal.viruses.Add(virus);
                Console.WriteLine("Cargaste exitosamente un nuevo virus");

            }
        }

        static void DestruyeVacunaOVirus()
        {
            Console.WriteLine("0 eliminar vacuna\n1 eliminar virus\no salir");
            string eleccion = SolicitaInputYControlaSeaValido("", 1);

            if (eleccion == "0")
            {
                Console.WriteLine("Indique el número de la vacuna a eliminar");
                int posicion = Convert.ToInt32(SolicitaInputYControlaSeaValido("", sucursal.vacunas.Count));
                sucursal.vacunas[posicion].codigo = "XXX";
                Console.WriteLine("se ha eliminado la vacuna");
            }
            else if (eleccion == "1")
            {
                Console.WriteLine("Indique el número del virus a eliminar");
                int posicion = Convert.ToInt32(SolicitaInputYControlaSeaValido("", sucursal.viruses.Count-1));
                sucursal.viruses[posicion].codigo = "YYYY";
                Console.WriteLine("se ha eliminado el virus");
            }
        }

        static void ImprimeVacunas()
        {
            Console.WriteLine("\nVacunas:\n   Nombre\tCodigo\t\tTipo");
            for (int i = 0; i < sucursal.vacunas.Count; i++)
            {
                Console.WriteLine($"{i} - {sucursal.vacunas[i].ToString()}");
            }
        }

        static void ImprimeVirus()
        {
            Console.WriteLine("\nVirus:\n   Nombre\tCodigo\t\tTipo");
            for (int i = 0; i < sucursal.viruses.Count; i++)
            {
                Console.WriteLine($"{i} - {sucursal.viruses[i].ToString()}");
            }
        }

        static bool BuscaVacunaActiva(List<Vacuna> vacunas)
        {
            int contador = 0;
            foreach (Vacuna vacuna in vacunas)
                if (vacuna.codigo != "XXX")
                    contador++;
            return (contador > 0);
        }


        static int PosicionUltimaVacunaActiva(List<Vacuna> vacunas)
        {
            string codigo = null;
            int i = vacunas.Count;

            while (codigo == null)
            {
                i--;
                if (vacunas[i].codigo != "XXX")
                    codigo = vacunas[i].codigo;
            }

            return i;
        }

        static string SolicitaInputYControlaSeaValido(string mensaje, int nMaximo)
        {
            string outputValido = null;

            while (outputValido == null)
            {
                Console.WriteLine(mensaje);
                string inputTexto = Console.ReadLine().Trim().ToUpper();
                outputValido = inputTexto == "S" ? inputTexto : null;

                if (int.TryParse(inputTexto, out int inputInt))
                    outputValido = (inputInt >= 0 && inputInt < nMaximo) ? inputTexto : null;
            }
            return outputValido;
        }
    }
}

