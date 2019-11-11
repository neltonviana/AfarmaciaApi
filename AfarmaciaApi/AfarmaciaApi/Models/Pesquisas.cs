using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace afarmaciaApi.Models
{
    public class Pesquisas
    {
        public long id { get; set; }
        public float? raio { get; set; }
        public long id_medicamento { get; set; }
        public long? id_farmacia { get; set; }
        public decimal ditancia_farmacia { get; set; }
        public decimal? preco_unitario { get; set; }
        public int? quantidade { get; set; }
        public decimal? total_valor { get; set; }
        public int? total_receita { get; set; }
        public int? total_encontrado { get; set; }
        public string forma { get; set; }
        public string dosagem { get; set; }
        public long id_registo { get; set; }
        public string data { get; set; }
        public long id_usuario { get; set; }
        public long id_fabricante { get; set; }
        public int referencia { get; set; }
    }
}