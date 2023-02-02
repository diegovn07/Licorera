using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class CarritoItemViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int idLicor { get; set; }

        [Display(Name = "Marca")]
        public string marca { get; set; }

        [Display(Name = "Tipo")]
        public string tipo { get; set; }

        [Display(Name = "Precio")]
        public int iPrecio { get; set; }

        [Display(Name = "Foto")]
        public string Foto_Licor { get; set; }

        [Display(Name = "Ml")]
        public int iMl { get; set; }
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }
        [Display(Name = "Licor")]
        public Licores licor { get; set; }
        [Display(Name = "Disponibilidad")]
        public bool disponibilidad { get; set; }
    }
}