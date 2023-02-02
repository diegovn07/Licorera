using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BackEnd.Entities;

namespace FrontEnd.Models
{
    public class RolesViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "Nombre")]
        public string NombreRol { get; set; }
        //public virtual ICollection<Users> Users { get; set; }
    }
}