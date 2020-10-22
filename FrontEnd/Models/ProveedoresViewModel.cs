using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class ProveedoresViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int idProveedor { get; set; }

        [Display(Name = "Nombre")]
        public string vNombre { get; set; }

        [Display(Name = "Cédula")]
        public int iCedula { get; set; }

        [Display(Name = "Teléfono")]
        public int iTelefono { get; set; }

        [Display(Name = "Email")]
        public string vCorreo { get; set; }

        [Display(Name = "Dirección")]
        public string vDireccion { get; set; }
    }
}