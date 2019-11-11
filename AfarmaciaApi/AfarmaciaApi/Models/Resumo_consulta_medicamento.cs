
namespace afarmaciaApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public partial class Resumo_consulta_medicamento
    {
        public long id { get; set; }
        public long? id_farmacia { get; set; }
        public Receita_real receita { get; set; }
        public float? raio { get; set; }
        public int? total_receita { get; set; }
        public decimal? total_valor { get; set; }
        public int? total_encontrado { get; set; }
    }
}