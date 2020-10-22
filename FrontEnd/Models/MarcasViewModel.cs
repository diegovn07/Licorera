using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class MarcasViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int idMarca { get; set; }
        [Display(Name = "Nombre")]
        public string vNombre { get; set; }
    }
}