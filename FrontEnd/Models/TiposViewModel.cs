using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class TiposViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int idTipo { get; set; }

        [Display(Name = "Nombre")]
        public string vNombre { get; set; }
    }
}