using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class LicoresViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int idLicor { get; set; }

        [Display(Name = "Marca")]
        public int idMarca { get; set; }

        [Display(Name = "Marca")]
        public Marcas marca{ get; set; }

        [Display(Name = "Marca")]
        public IEnumerable<Marcas> marcas { get; set; }

        [Display(Name = "Tipo")]
        public int idTipo { get; set; }

        [Display(Name = "Tipo")]
        public Tipos tipo { get; set; }

        [Display(Name = "Tipo")]
        public IEnumerable<Tipos> tipos { get; set; }

        [Display(Name = "Proveedor")]
        public int idProveedor { get; set; }

        [Display(Name = "Proveedor")]
        public Proveedores proveedor { get; set; }

        [Display(Name = "Proveedor")]
        public IEnumerable<Proveedores> proveedores { get; set; }

        [Display(Name = "Descripción")]
        public string vDescripción { get; set; }

        [Display(Name = "U. Disp.")]
        public int iUnidades { get; set; }

        [Display(Name = "Precio")]
        public int iPrecio { get; set; }

        [Display(Name = "Foto")]
        public HttpPostedFileBase Foto { get; set; }

        [Display(Name = "Foto")]
        public string Foto_Licor { get; set; }

        [Display(Name = "Ml")]
        public int iMl { get; set; }
    }
}