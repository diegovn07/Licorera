using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class DetallePedidoViewModel
    {
        [Key]
        public int idDetalle { get; set; }

        [Display(Name = "Número de Pedido")]
        public int idPedido { get; set; }

        [Display(Name = "Detalle")]
        public int idLicor { get; set; }
        public LicoresViewModel licor { get; set; }

        [Display(Name = "Precio")]
        public int iPrecio { get; set; }

        [Display(Name = "Cantidad")]
        public int iCantidad { get; set; } 
    }
}