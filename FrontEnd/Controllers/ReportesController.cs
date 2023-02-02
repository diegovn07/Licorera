using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class ReportesController : Controller
    {
        private PedidosViewModel Convertir(Pedidos pedido)
        {
            PedidosViewModel pedidosViewModel = new PedidosViewModel
            {
                idPedido = pedido.idPedido,
                vNombre_Cliente = pedido.vNombre_Cliente,
                vTelefono = pedido.vTelefono,
                vEmail = pedido.vEmail,
                vDireccion_Envio = pedido.vDireccion_Envio,
                dFecha = pedido.dFecha,
                idEstado_Pedido = pedido.idEstado_Pedido,
                idMedio_Pago = pedido.idMedio_Pago,
                bEstado_Pago = pedido.bEstado_Pago,
                identificacion = pedido.identificacion
            };
            return pedidosViewModel;
        }
        // GET: Reportes
        public ActionResult Index()
        {
            //List<Pedidos> pedidos;
            //using (UnidadDeTrabajo<Pedidos> Unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            //{
            //    pedidos = Unidad.genericDAL.GetAll().ToList();
            //}

            //List<estados_Pedido> estados;
            //using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
            //{
            //    estados = Unidad.genericDAL.GetAll().ToList();
            //}

            //reporteViewModel reporte = new reporteViewModel
            //{
            //    estados = estados,
            //    data = pedidos
            //};
            return View();
        }

        [HttpPost]
        public JsonResult getData()
        {
            List<Pedidos> pedidos;
            using (UnidadDeTrabajo<Pedidos> Unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                pedidos = Unidad.genericDAL.GetAll().ToList();
            }

            List<estados_Pedido> estados;
            using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
            {
                estados = Unidad.genericDAL.GetAll().ToList();
            }

            List<reporteViewModel> labels = new List<reporteViewModel>();
            for (var i = 0; i < estados.Count; i++)
            {
                reporteViewModel item = new reporteViewModel
                {
                    label = estados[i].vNombre,
                    cantidad = pedidos.Count(element => element.idEstado_Pedido == estados[i].idEstado)
                };
                labels.Add(item);
            };
            return Json(new
            {
                status = true,
                items = labels
            }); ;
        }

    }
}