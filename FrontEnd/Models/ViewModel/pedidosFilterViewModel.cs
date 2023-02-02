using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FrontEnd.Models.ViewModel
{
    public class pedidosFilterViewModel
    {
        public List<PedidosViewModel> lista { get; set; }

        [Display(Name = "Estado del Pedido")]
        public List<estados_Pedido> estados { get; set; }
    }
}