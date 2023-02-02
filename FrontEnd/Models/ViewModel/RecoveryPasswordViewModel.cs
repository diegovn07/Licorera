using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModel
{
    public class RecoveryPasswordViewModel
    {
        public string token { get; set; }
        [Required]
        [Display(Name = "Confirmar contraseña")]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        [Display(Name = "Contraseña")]
        public string Password2 { get; set; }
    }
}