using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace afarmaciaApi.Models
{
    public class Receita_real
    {
        public Receita receita { get; set; }
        public Medicamento_farmacia medicamento { get; set; }
        public Nullable<int> total { get; set; }
    }
}