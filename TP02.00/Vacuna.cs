using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RollandiAlejandro
{
    internal class Vacuna : Sucursal
    {
        public string nombre;
        public string identificador;
        public Tipo tipo;

        public Vacuna()
        {
        }

        public Vacuna(List<Vacuna> vacunas)
        {
            CargaNombre();
            CargaIdentificador(vacunas);
            CargaTipo();
        }

        public void CargaNombre()
        {
            string nombre = null;
            while (nombre == null)
            {
                nombre = SolicitaInput("\nDefina el nombre");
                nombre = ValidaLongitudPositiva(nombre) ? nombre : null;
            }
            nombre = this.nombre;
        }

        public void CargaIdentificador(List<Vacuna> vacunas)
        {
            string identificador = null;
            while (identificador == null)
            {
                identificador = SolicitaInput("\nDefina el código único");
                identificador = ValidaLongitudPositiva(identificador) ? identificador : null;
                identificador = ValidaQueNoSeaCodigoBaja(identificador) ? identificador : null;
                identificador = ValidaUnicidad(vacunas, identificador) ? identificador : null;
            }
            identificador = this.identificador;
        }

        public void CargaTipo()
        {
            ImprimeEnumTipo();
            string tipoString = null;
            Tipo tipo = 0;

            while (tipoString == null)
            {
                tipoString = SolicitaInput("\nElija alguno de estos tipos");
                tipoString = Enum.TryParse<Tipo>(tipoString, true, out tipo) ? tipoString : null;
            }
            tipo = this.tipo;
        }

        protected string SolicitaInput(string mensaje)
        {
            Console.WriteLine(mensaje);
            return Console.ReadLine().Trim().ToUpper();
        }

        bool ValidaLongitudPositiva(string input)
        {
            return (input.Length > 0);
        }

        protected bool ValidaQueNoSeaCodigoBaja(string codigo)
        {
            return (codigo.ToUpper() != "XXX" && codigo.ToUpper() != "YYYY");
        }

        protected bool ValidaUnicidad(List<Vacuna> vacunas, string identificador)
        {
            int contador = 0;
            foreach (Vacuna vacuna in vacunas)
                if (vacuna.identificador == identificador)
                    contador++;
            return (contador == 0);
        }

        private void ImprimeEnumTipo()
        {
            Array tipos = Enum.GetValues(typeof(Tipo));
            foreach (Tipo tipo in tipos)
                Console.WriteLine(tipo);
        }

        public override string ToString()
        {
            return $"{codigo}\t\t{identificador}\t\t{tipo}";
        }

        //es para crear vacunas con datos automáticos
        public Vacuna(int a, int b)
        {
            nombre = CreaCodigoAlfanumericoAleatorio(a);
            identificador = CreaCodigoAlfanumericoAleatorio(b);
            tipo = EligeUnTipoAleatoriamente();
        }

        protected Tipo EligeUnTipoAleatoriamente()
        {
            Random random = new Random();
            int tipoIndex = random.Next(Enum.GetValues(typeof(Tipo)).Length);
            return (Tipo)Enum.GetValues(typeof(Tipo)).GetValue(tipoIndex);
        }
    }
}
