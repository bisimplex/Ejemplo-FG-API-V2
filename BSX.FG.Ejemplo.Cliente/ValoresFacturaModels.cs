using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSX.FG.Ejemplo.Cliente
{
    public class ValoresApiModel
    {
        public string Url { get; set; }

        public string ApiKey { get; set; }
    }

    public class ConceptoModel
    {
        public decimal Precio { get; set; }

        public string ConceptoId { get; set; }

        public string Descripcion { get; internal set; }
    }
}
