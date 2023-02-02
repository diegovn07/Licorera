using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class licoresTiendaViewModel
    {
        public List<LicoresViewModel> lista { get; set; }

        public List<Tipos> tipos { get; set; }
    }
}