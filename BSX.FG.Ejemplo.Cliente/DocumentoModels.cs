using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSX.FG.Ejemplo.Cliente
{
    public class DocumentoRow
    {
        public bool Borrador { get; set; }

        public int UserId { get; set; }

        public int ReceptorId { get; set; }

        public string DocumentoId { get; set; }

        public DateTime Fecha { get; set; }

        public string Folio { get; set; }

        public string Serie { get; set; }

        public string FolioCompleto { get; set; }

        public string Moneda { get; set; }

        public string Receptor { get; set; }

        public string RfcReceptor { get; set; }

        public decimal Total { get; set; }

        public string TotalCadena { get; set; }

        public string UUID { get; set; }

        public string Version { get; set; }

    }

    public class DocumentoSimpleModel
    {
        public bool Borrador => false;

        public string CondicionesPago { get; set; }

        public string CorreoElectronico { get; set; }

        public string Fecha => DateTime.Now.ToShortDateString();

        public string FormaPago { get; set; }

        public string MetodoPago => "PUE";

        public string Moneda => "MXN";

        public string Notas { get; set; }

        public string ReceptorId { get; set; }

        public string RegimenFiscal { get; set; }

        public string SucursalId { get; set; }

        public string TipoCambio => "1";

        public string TipoDocumentoId => "1";// factura

        public string UsoCFDI => "G03";

        public IEnumerable<ConceptoSipleModel> Conceptos { get; set; }
    }

    public class DocumentoLocalModel
    {
        public string DocumentoId { get; set; }
    }

    public class ConceptoSipleModel
    {
        public decimal Cantidad { get; set; }

        public string ConceptoId { get; set; }

        public string Descripcion { get; set; }

        public decimal Descuento { get; set; }

        public decimal Precio { get; set; }
    }
}
