//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BackEnd.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Licores
    {
        public int idLicor { get; set; }
        public string vNombre { get; set; }
        public int idMarca { get; set; }
        public int idTipo { get; set; }
        public int idProveedor { get; set; }
        public string vDescripción { get; set; }
        public int iUnidades { get; set; }
        public int iPrecio { get; set; }
        public string Foto_Licor { get; set; }
    
        public virtual Marcas Marcas { get; set; }
        public virtual Proveedores Proveedores { get; set; }
        public virtual Tipos Tipos { get; set; }
    }
}
