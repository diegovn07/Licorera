using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using BackEnd.Entities;

namespace FrontEnd.Models
{
    public class UserViewModel
    {
        [Display(Name = "Identificador")]
        [Key]
        public int UserId { get; set; }
        [Display(Name = "Usuario")]
        [Required(ErrorMessage = "Debe digitar nombre de usuario.")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Debe digitar una Contraseña.")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe indicar su cédula")]
        [Display(Name = "Cedula")]
        public string cedula { get; set; }

        [Required(ErrorMessage = "Debe indicar su nombre")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Display(Name = "Apellido")]
        public string apellido1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        public string apellido2 { get; set; }

        [Required(ErrorMessage = "Debe indicar un correo")]
        [Display(Name = "Correo")]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }
        [Display(Name = "Direccion")]
        [Required(ErrorMessage = "Debe indicar su dirección")]
        public string direccion { get; set; }

        [Required(ErrorMessage = "Debe indicar su teléfono")]
        [Display(Name = "Telefono")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "No se permiten caractéres")]
        public string telefono { get; set; }
        [Display(Name = "Telefono opcional")]
        [DataType(DataType.PhoneNumber)]
        public string telefono2 { get; set; }
        public String LoginErrorMensaje { get; set; }
        public virtual ICollection<Roles> Roles { get; set; }

        //USUARIO ADMINISTRADOR
        [Display(Name = "Usuario Administrador")]
        public bool UsuarioAdmin { get; set; }
    }
}