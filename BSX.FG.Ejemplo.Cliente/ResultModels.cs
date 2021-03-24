using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSX.FG.Ejemplo.Cliente
{
    public class ResultModel
    {
        public bool Success { get; set; }

        public string ErrorCodeName { get; set; }

        public int ErrorCode { get; set; }

        public string SingleLineErrors { get; set; }

        public IEnumerable<string> Messages { get; set; }
    }

    public class ResultModel<T> : ResultModel
    {
        public T Content { get; set; }
    }
}
