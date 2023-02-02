using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BackEnd.DAL;
using BackEnd.Entities;
using FrontEnd.Models;

namespace FrontEnd.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private LicoresViewModel Convertir(Licores licor)
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

        private Licores Convertir(LicoresViewModel licorViewModel)
        {
            Licores licor = new Licores
            {
                idLicor = licorViewModel.idLicor,
                idMarca = (int)licorViewModel.idMarca,
                idTipo = (int)licorViewModel.idTipo,
                idProveedor = (int)licorViewModel.idProveedor,
                vDescripción = licorViewModel.vDescripción,
                iUnidades = licorViewModel.iUnidades,
                iPrecio = licorViewModel.iPrecio,
                Foto_Licor = licorViewModel.Foto_Licor,
                Foto_Detalles = licorViewModel.Foto_Detalles,
                iMl = (int)licorViewModel.iMl
            };
            return licor;
        }

        public ActionResult Index(List<int> filt = null)
        {
            
            List<Licores> licores;
            if (filt == null)
            {
                using (UnidadDeTrabajo<Licores> Unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                {
                    licores = Unidad.genericDAL.GetAll().ToList();
                }
            }
            else {
                if (filt[0] != 0)
                {
                    using (UnidadDeTrabajo<Licores> Unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                    {
                        licores = Unidad.genericDAL.GetAll().ToList();
                        for (var i = 0; i < filt.Count; i++)
                        {
                            licores = licores.Where(licor => licor.idTipo == filt[i]).ToList();
                        }
                    }
                }
                else {
                    using (UnidadDeTrabajo<Licores> Unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
                    {
                        licores = Unidad.genericDAL.GetAll().ToList();
                    }
                }

                
            }
            List<LicoresViewModel> lista = new List<LicoresViewModel>();
            LicoresViewModel licorViewModel;
            foreach (var item in licores)
            {
                licorViewModel = this.Convertir(item);

                using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
                {
                    licorViewModel.marca = Unidad.genericDAL.Get(item.idMarca);
                }

                using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
                {
                    licorViewModel.tipo = Unidad.genericDAL.Get(item.idTipo);
                }

                using (UnidadDeTrabajo<Proveedores> Unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
                {
                    licorViewModel.proveedor = Unidad.genericDAL.Get(item.idProveedor);
                }

                lista.Add(licorViewModel);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Views/Home/licoresPartial.cshtml", lista);
            }
            else {
                List<Tipos> tipos = new List<Tipos>();
                using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
                {
                    tipos = Unidad.genericDAL.GetAll().ToList();
                }
                licoresTiendaViewModel data = new licoresTiendaViewModel
                {
                    lista = lista,
                    tipos = tipos
                };
                return View(data);
            }
            
        }

        public ActionResult Details(int id) /*Agregué este details para hacer un boton de los detalles del producto para agrregarlo al carrito*/
        {

            Licores licor;
            using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                licor = unidad.genericDAL.Get(id);
            }

            LicoresViewModel licorViewModel = this.Convertir(licor);

            using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                licorViewModel.marca = Unidad.genericDAL.Get(licor.idMarca);
            }

            using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                licorViewModel.tipo = Unidad.genericDAL.Get(licor.idTipo);
            }

            using (UnidadDeTrabajo<Proveedores> Unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                licorViewModel.proveedor = Unidad.genericDAL.Get(licor.idProveedor);
            }

            return View(licorViewModel);
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult About()
        {

            return View();
        }
    }
}