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
    
    public partial class Seguro_empresa_usuario_depedente
    {
        public long id_usuario { get; set; }
        public long id_seguradora { get; set; }
        public long id_empresa { get; set; }
        public int id_tipo { get; set; }
        public int id_categoria { get; set; }
        public long id_dependente { get; set; }
        public string cobertura { get; set; }
        public Nullable<System.DateTime> registo { get; set; }
        public Nullable<System.DateTime> actualizacao { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> situacao { get; set; }
    
        public virtual Dependente Dependente { get; set; }
        public virtual Seguro_empresa_usuario Seguro_empresa_usuario { get; set; }
    }
}
