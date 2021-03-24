using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSX.FG.Ejemplo.Cliente
{
    public class ClienteRow
    {
        public int ClienteId { get; set; }

        public string Alias { get; set; }

        public string CURP { get; set; }

        public string Nombre { get; set; }

        public string RFC { get; set; }
    }

    public class ClienteSimpleModel
    {
        public int ClienteId { get; set; }

        public string Nombre { get; set; }

        public string RFC { get; set; }

        public string CorreoContacto { get; set; }

        public string NombreContacto { get; set; }
    }
}
