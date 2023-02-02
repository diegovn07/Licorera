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
    public class CarritoController : Controller
    {

        // GET: Carrito
        public ActionResult Index()
        {
            List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
            return View(carrito);
        }

        [HttpPost]
        public JsonResult agregarCarrito(CarritoItemViewModel item)
        {
            try {
                List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
                CarritoItemViewModel c = carrito.Find(licor => licor.idLicor == item.idLicor);
                if (c != null)
                {
                    carrito[carrito.IndexOf(c)].cantidad = carrito[carrito.IndexOf(c)].cantidad + item.cantidad;
                }
                else {
                    carrito.Add(item);
                }

                Session["Carrito"] = carrito;
                return Json(new
                {
                    message = "Agregado",
                    status = true
                });
            } catch (Exception error) {
                return Json(new
                {
                    message = error.Message,
                    status = false
                });
            }
            
            //return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult eliminarCarrito(CarritoItemViewModel licor)
        {
            try
            {
                List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
                carrito.RemoveAll(item => item.idLicor == licor.idLicor);
                Session["Carrito"] = carrito;
                return Json(new
                {
                    message = "Eliminado",
                    status = true
                });
            }
            catch (Exception error)
            {
                return Json(new
                {
                    message = error.Message,
                    status = false
                });
            }

            //return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult sumarCarrito(CarritoItemViewModel licor)
        {
            try
            {
                List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
                CarritoItemViewModel c = carrito.Find(item => item.idLicor == licor.idLicor);
                int nuevaCantidad = 0;
                int nuevoTotal = 0;
                int nuevoTinalFinal = 0;
                if (c != null)
                {
                    carrito[carrito.IndexOf(c)].cantidad = carrito[carrito.IndexOf(c)].cantidad + 1;
                    nuevaCantidad = carrito[carrito.IndexOf(c)].cantidad;
                    nuevoTotal = carrito[carrito.IndexOf(c)].iPrecio * nuevaCantidad;
                    foreach (var item in carrito) {
                        nuevoTinalFinal = nuevoTinalFinal + (item.cantidad * item.iPrecio);
                    }
                }
                Session["Carrito"] = carrito;
                return Json(new
                {
                    cantidad = nuevaCantidad,
                    total = nuevoTotal,
                    totalFinal = nuevoTinalFinal,
                    status = true
                });
            }
            catch (Exception error)
            {
                return Json(new
                {
                    message = error.Message,
                    status = false
                });
            }

            //return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult restarCarrito(CarritoItemViewModel licor)
        {
            try
            {
                List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
                CarritoItemViewModel c = carrito.Find(item => item.idLicor == licor.idLicor);
                int nuevaCantidad = 0;
                int nuevoTotal = 0;
                int nuevoTinalFinal = 0;
                if (c != null)
                {
                    carrito[carrito.IndexOf(c)].cantidad = carrito[carrito.IndexOf(c)].cantidad - 1;
                    nuevaCantidad = carrito[carrito.IndexOf(c)].cantidad;
                    nuevoTotal = carrito[carrito.IndexOf(c)].iPrecio * nuevaCantidad;
                    foreach (var item in carrito)
                    {
                        nuevoTinalFinal = nuevoTinalFinal + (item.cantidad * item.iPrecio);
                    }
                }
                Session["Carrito"] = carrito;
                return Json(new
                {
                    cantidad = nuevaCantidad,
                    total = nuevoTotal,
                    totalFinal = nuevoTinalFinal,
                    status = true
                });
            }
            catch (Exception error)
            {
                return Json(new
                {
                    message = error.Message,
                    status = false
                });
            }

            //return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult actualizarCarrito(List<CarritoItemViewModel> licores)
        {
            try
            {
                List<CarritoItemViewModel> nuevoCarrito = new List<CarritoItemViewModel>();
                int nuevoTotal = 0;
                if (licores != null)
                {
                    List<CarritoItemViewModel> carrito = Session["Carrito"] as List<CarritoItemViewModel>;
                    foreach (var item in licores)
                    {
                        CarritoItemViewModel c = carrito.Find(element => element.idLicor == item.idLicor);
                        carrito[carrito.IndexOf(c)].cantidad = item.cantidad;
                        nuevoCarrito.Add(c);
                    }
                    foreach (var item in nuevoCarrito)
                    {
                        nuevoTotal = nuevoTotal + (item.cantidad * item.iPrecio);
                    }

                }
                Session["Carrito"] = nuevoCarrito;
                return Json(new
                {
                    status = true,
                    total = nuevoTotal,
                    message = "Se actualizó el carrito"
                });
            }
            catch (Exception error)
            {
                return Json(new
                {
                    message = error.Message,
                    status = false
                });
            }
        }
    }
}