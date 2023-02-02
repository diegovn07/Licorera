using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class PedidosViewModel
    {
        [Key]
        public int idPedido { get; set; }

        [Required(ErrorMessage = "Debe digitar su nombre.")]
        [Display(Name = "Nombre")]
        public string vNombre_Cliente { get; set; }

        [Required(ErrorMessage = "Debe indicar un teléfono de contacto.")]
        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{2})$", ErrorMessage = "Teléfono inválido")]
        public string vTelefono { get; set; }

        [Required(ErrorMessage = "Debe indicar un correo de contacto.")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string vEmail { get; set; }

        [Required(ErrorMessage = "Debe indicar la dirección de envío")]
        [Display(Name = "Dirección de Envío")]
        public string vDireccion_Envio { get; set; }

        [Display(Name = "Fecha de Pedido")]
        public System.DateTime dFecha { get; set; }

        [Display(Name = "Estado")]
        public Nullable<int> idEstado_Pedido { get; set; }

        public IEnumerable<estados_Pedido> estados { get; set; }

        public estados_Pedido estado { get; set; }


        [Required(ErrorMessage = "Debe indicar un medio de pago.")]
        [Display(Name = "Medio de Pago")]
        public Nullable<int> idMedio_Pago { get; set; }

        public IEnumerable<medios_Pago> medios { get; set; }

        public medios_Pago medio { get; set; }

        [Display(Name = "Pagado?")]
        public bool bEstado_Pago { get; set; }

        [Required(ErrorMessage = "Debe indicar su identificación.")]
        [Display(Name = "Identificación")]
        public string identificacion { get; set; }

        [Display(Name = "Crear Cuenta")]
        public bool crearCuenta { get; set; }
        public List<DetallePedidoViewModel> lineas { get; set; }
    }
}