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
    
    public partial class Pedidos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedidos()
        {
            this.Detalles_Pedido = new HashSet<Detalles_Pedido>();
        }
    
        public int idPedido { get; set; }
        public string vNombre_Cliente { get; set; }
        public string vTelefono { get; set; }
        public string vEmail { get; set; }
        public string vDireccion_Envio { get; set; }
        public System.DateTime dFecha { get; set; }
        public Nullable<int> idEstado_Pedido { get; set; }
        public Nullable<int> idMedio_Pago { get; set; }
        public bool bEstado_Pago { get; set; }
        public string identificacion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detalles_Pedido> Detalles_Pedido { get; set; }
        public virtual estados_Pedido estados_Pedido { get; set; }
        public virtual medios_Pago medios_Pago { get; set; }
    }
}
