using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;
using FrontEnd.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class PedidosController : Controller
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

        private Pedidos Convertir(PedidosViewModel pedidosViewModel)
        {
            Pedidos pedido = new Pedidos
            {
                idPedido = pedidosViewModel.idPedido,
                vNombre_Cliente = pedidosViewModel.vNombre_Cliente,
                vTelefono = pedidosViewModel.vTelefono,
                vEmail = pedidosViewModel.vEmail,
                vDireccion_Envio = pedidosViewModel.vDireccion_Envio,
                dFecha = pedidosViewModel.dFecha,
                idEstado_Pedido = pedidosViewModel.idEstado_Pedido,
                idMedio_Pago = pedidosViewModel.idMedio_Pago,
                bEstado_Pago = pedidosViewModel.bEstado_Pago,
                identificacion = pedidosViewModel.identificacion
            };
            return pedido;
        }

        private DetallePedidoViewModel convertirDetalles(Detalles_Pedido detalle)
        {
            DetallePedidoViewModel detallePedidoViewModel = new DetallePedidoViewModel
            {
                idDetalle = detalle.idDetalle,
                idPedido = detalle.idPedido,
                idLicor = detalle.idLicor,
                iPrecio = detalle.iPrecio,
                iCantidad = detalle.iCantidad
            };
            return detallePedidoViewModel;
        }

        private LicoresViewModel ConvertirLicores(Licores licor)
        {
            LicoresViewModel licorViewModel = new LicoresViewModel
            {
                idLicor = licor.idLicor,
                idMarca = (int)licor.idMarca,
                idTipo = (int)licor.idTipo,
                idProveedor = (int)licor.idProveedor,
                vDescripción = licor.vDescripción,
                iUnidades = licor.iUnidades,
                iPrecio = licor.iPrecio,
                Foto_Licor = licor.Foto_Licor,
                Foto_Detalles = licor.Foto_Detalles,
                iMl = (int)licor.iMl,
            };
            return licorViewModel;
        }

        // GET: Pedidos
        [HttpGet]
        public ActionResult Index(int estado = 1)
        {
            List<Pedidos> pedidos;
            if (estado == 0)
            {
                using (UnidadDeTrabajo<Pedidos> Unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
                {
                    pedidos = Unidad.genericDAL.GetAll().ToList();
                }
            }
            else {
                using (UnidadDeTrabajo<Pedidos> Unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
                {
                    pedidos = Unidad.genericDAL.Find(item => item.idEstado_Pedido == estado).ToList();
                }
            }
            
            List<PedidosViewModel> lista = new List<PedidosViewModel>();
            PedidosViewModel pedidoViewModel;
            foreach (var item in pedidos) {
                pedidoViewModel = Convertir(item);

                using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
                {
                    pedidoViewModel.estado = Unidad.genericDAL.Get((int)pedidoViewModel.idEstado_Pedido);
                }

                using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
                {
                    pedidoViewModel.medio = Unidad.genericDAL.Get((int)pedidoViewModel.idMedio_Pago);
                }
                lista.Add(pedidoViewModel);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Pedidos/pedidosPartial.cshtml", lista);
            }
            else
            {
                List<estados_Pedido> estados = new List<estados_Pedido>();
                using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
                {
                    estados = Unidad.genericDAL.GetAll().ToList();
                }
                estados.Add(new estados_Pedido
                {
                    idEstado = 0,
                    vNombre = "Todos"
                });
                pedidosFilterViewModel items = new Models.ViewModel.pedidosFilterViewModel
                {
                    lista = lista,
                    estados = estados
                };
                return View(items);
            }
        }

        //public ActionResult Create()
        //{
        //    PedidosViewModel pedido = new PedidosViewModel();
        //    using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
        //    {
        //        pedido.medios = Unidad.genericDAL.GetAll();
        //    }
        //    return View(pedido);
        //}

        //[HttpPost]
        //public ActionResult Create(PedidosViewModel pedidoViewModel)
        //{
        //    Pedidos pedido = this.Convertir(pedidoViewModel);

        //    using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
        //    {
        //        unidad.genericDAL.Add(pedido);
        //        unidad.Complete();
        //    }

        //    return RedirectToAction("Index");
        //}

        public ActionResult Edit(int id)
        {

            Pedidos pedido;
            using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                pedido = unidad.genericDAL.Get(id);

            }

            PedidosViewModel ped = this.Convertir(pedido);
            using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
            {
                ped.medios = Unidad.genericDAL.GetAll();
            }

            using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
            {
                ped.estados = Unidad.genericDAL.GetAll();
            }

            return View(ped);
        }

        [HttpPost]
        public ActionResult Edit(PedidosViewModel pedidoViewModel)
        {


            using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(pedidoViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            Pedidos pedido;
            using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                pedido = unidad.genericDAL.Get(id);

            }

            PedidosViewModel ped = this.Convertir(pedido);
            using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
            {
                ped.medio = Unidad.genericDAL.Get((int)pedido.idMedio_Pago);
            }

            using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
            {
                ped.estado = Unidad.genericDAL.Get((int)pedido.idEstado_Pedido);
            }

            List<Detalles_Pedido> lineas;
            using (UnidadDeTrabajo<Detalles_Pedido> Unidad = new UnidadDeTrabajo<Detalles_Pedido>(new BDContext()))
            {
                lineas = Unidad.genericDAL.Find(item => item.idPedido == pedido.idPedido).ToList();
            }

            List<DetallePedidoViewModel> lin = new List<DetallePedidoViewModel>();
            foreach (var item in lineas)
            {
                DetallePedidoViewModel nueva = convertirDetalles(item);
                using (UnidadDeTrabajo<Licores> Unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                {
                    nueva.licor = ConvertirLicores(Unidad.genericDAL.Get(nueva.idLicor));
                }

                using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
                {
                    nueva.licor.marca = Unidad.genericDAL.Get(nueva.licor.idMarca);
                }

                using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
                {
                    nueva.licor.tipo = Unidad.genericDAL.Get(nueva.licor.idTipo);
                }

                lin.Add(nueva);
            }

            ped.lineas = lin;

            return View(ped);
        }

        public ActionResult Delete(int id)
        {

            Pedidos pedido;
            using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                pedido = unidad.genericDAL.Get(id);

            }

            PedidosViewModel ped = this.Convertir(pedido);
            using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
            {
                ped.medio = Unidad.genericDAL.Get((int)pedido.idMedio_Pago);
            }

            using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
            {
                ped.estado = Unidad.genericDAL.Get((int)pedido.idEstado_Pedido);
            }
            return View(ped);
        }

        [HttpPost]
        public ActionResult Delete(PedidosViewModel pedidoViewModel)
        {
            using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(pedidoViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult userConfirmData()
        {
            PedidosViewModel pedido = new PedidosViewModel();

            using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
            {
                pedido.medios = Unidad.genericDAL.GetAll();
            }

            if (Request.IsAuthenticated && Request.Cookies["userInfo"] != null)
            {
                var userInfo = Request.Cookies["userInfo"]["UserID"];

                if (!string.IsNullOrEmpty(userInfo))
                {
                    var id = Int32.Parse(userInfo.ToString());
                    Users usuario;
                    using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))
                    {

                        usuario = unidad.genericDAL.Get(id);
                        pedido.vNombre_Cliente = usuario.nombre + ' ' + usuario.apellido1 + ' ' + usuario.apellido2;
                        pedido.vTelefono = usuario.telefono;
                        pedido.vEmail = usuario.Correo;
                        pedido.vDireccion_Envio = usuario.direccion;
                        pedido.identificacion = usuario.cedula;
                    }
                }
            }
            return View(pedido);

        }

        [HttpPost]
        public ActionResult userConfirmData(PedidosViewModel pedidosViewModel)
        {
            bool verificator = true;
            List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
            int i = 0;
            while (i < carrito.Count)
            {
                using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                {
                    carrito[i].licor = unidad.genericDAL.Get(carrito[i].idLicor);
                }

                if (carrito[i].licor.iUnidades < carrito[i].cantidad)
                {
                    carrito[i].disponibilidad = false;
                    verificator = false;
                }
                else {
                    carrito[i].disponibilidad = true;
                }
                i = i + 1;
            }

            if (verificator)
            {
                pedidosViewModel.dFecha = DateTime.Now;
                pedidosViewModel.idEstado_Pedido = 1;
                pedidosViewModel.bEstado_Pago = false;
                using (PedidosDALImpl unidad = new PedidosDALImpl(new BDContext()))
                {
                    pedidosViewModel.idPedido = unidad.Add(this.Convertir(pedidosViewModel));
                }

                List<Detalles_Pedido> lineas = new List<Detalles_Pedido>();
                foreach (var item in carrito)
                {
                    Detalles_Pedido nuevaLinea = new Detalles_Pedido
                    {
                        idPedido = pedidosViewModel.idPedido,
                        idLicor = item.idLicor,
                        iPrecio = item.iPrecio,
                        iCantidad = item.cantidad
                    };
                    lineas.Add(nuevaLinea);

                    using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                    {
                        item.licor.iUnidades = item.licor.iUnidades - item.cantidad;
                        unidad.genericDAL.Update(item.licor);
                        unidad.Complete();
                    }
                }

                using (UnidadDeTrabajo<Detalles_Pedido> unidad = new UnidadDeTrabajo<Detalles_Pedido>(new BDContext()))
                {
                    unidad.genericDAL.AddRange(lineas);
                    unidad.Complete();
                }
                carrito.Clear();
                Session["Carrito"] = carrito;
                return RedirectToAction("thanksPage", pedidosViewModel);
            }
            else {
                Session["Carrito"] = carrito;
                return RedirectToAction("sorryPage");
            }
        }

        public ActionResult thanksPage(PedidosViewModel pedidosViewModel)
        {
            using (UnidadDeTrabajo<estados_Pedido> Unidad = new UnidadDeTrabajo<estados_Pedido>(new BDContext()))
            {
                pedidosViewModel.estado = Unidad.genericDAL.Get((int)pedidosViewModel.idEstado_Pedido);
            }

            using (UnidadDeTrabajo<medios_Pago> Unidad = new UnidadDeTrabajo<medios_Pago>(new BDContext()))
            {
                pedidosViewModel.medio = Unidad.genericDAL.Get((int)pedidosViewModel.idMedio_Pago);
            }

            List<Detalles_Pedido> lineas;
            using (UnidadDeTrabajo<Detalles_Pedido> Unidad = new UnidadDeTrabajo<Detalles_Pedido>(new BDContext()))
            {
                lineas = Unidad.genericDAL.Find(item => item.idPedido == pedidosViewModel.idPedido).ToList();
            }

            List<DetallePedidoViewModel> lin = new List<DetallePedidoViewModel>();
            foreach (var item in lineas) {
                DetallePedidoViewModel nueva = convertirDetalles(item);
                using (UnidadDeTrabajo<Licores> Unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                {
                    nueva.licor = ConvertirLicores(Unidad.genericDAL.Get(nueva.idLicor));
                }

                using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
                {
                    nueva.licor.marca = Unidad.genericDAL.Get(nueva.licor.idMarca);
                }

                using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
                {
                    nueva.licor.tipo = Unidad.genericDAL.Get(nueva.licor.idTipo);
                }
                lin.Add(nueva);
            }

            pedidosViewModel.lineas = lin;

            return View(pedidosViewModel);
        }

        public ActionResult sorryPage() {
            List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
            return View(carrito);
        }

    }
}