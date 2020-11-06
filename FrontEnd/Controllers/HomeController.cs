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
                iMl = (int)licorViewModel.iMl
            };
            return licor;
        }
        public ActionResult Index()
        {
            List<Licores> licores;
            using (UnidadDeTrabajo<Licores> Unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                licores = Unidad.genericDAL.GetAll().ToList();
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
            return View(lista);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}