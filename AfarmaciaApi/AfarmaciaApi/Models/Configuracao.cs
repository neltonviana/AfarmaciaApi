//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace afarmaciaApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Configuracao
    {
        public long id_configuracao { get; set; }
        public Nullable<System.DateTime> data { get; set; }
        public Nullable<System.DateTime> actualizacao { get; set; }
        public Nullable<int> medicamentos_receita { get; set; }
        public Nullable<int> medicamento_dia { get; set; }
        public Nullable<int> usuarios_farmacia { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> situacao { get; set; }
        public Nullable<int> id_categoria { get; set; }
    
        public virtual Categoria_usuario Categoria_usuario { get; set; }
    }
}
