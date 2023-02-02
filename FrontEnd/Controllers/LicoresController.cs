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
    [Authorize(Roles = "Administrador")]
    public class LicoresController : Controller
    {
       
        private LicoresViewModel Convertir(Licores licor)
        {
            LicoresViewModel licorViewModel = new LicoresViewModel
            {
                idLicor = licor.idLicor,
                idMarca = (int) licor.idMarca,
                idTipo = (int) licor.idTipo,
                idProveedor = (int) licor.idProveedor,
                vDescripción = licor.vDescripción,
                iUnidades = licor.iUnidades,
                iPrecio = licor.iPrecio,
                Foto_Licor = licor.Foto_Licor,
                Foto_Detalles = licor.Foto_Detalles,
                iMl = (int) licor.iMl,
            };
            return licorViewModel;
        }

        private Licores Convertir(LicoresViewModel licorViewModel)
        {
            Licores licor = new Licores
            {
                idLicor = licorViewModel.idLicor,
                idMarca = (int) licorViewModel.idMarca,
                idTipo = (int) licorViewModel.idTipo,
                idProveedor = (int) licorViewModel.idProveedor,
                vDescripción = licorViewModel.vDescripción,
                iUnidades = licorViewModel.iUnidades,
                iPrecio = licorViewModel.iPrecio,
                Foto_Licor = licorViewModel.Foto_Licor,
                Foto_Detalles = licorViewModel.Foto_Detalles,
                iMl = (int) licorViewModel.iMl
            };
            return licor;
        }

        // GET: Licores
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

        public ActionResult Create()
        {
            LicoresViewModel licorViewModel = new LicoresViewModel();
            using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                licorViewModel.marcas = Unidad.genericDAL.GetAll();
            }

            using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                licorViewModel.tipos = Unidad.genericDAL.GetAll();
            }

            using (UnidadDeTrabajo<Proveedores> Unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                licorViewModel.proveedores = Unidad.genericDAL.GetAll();
            }
            return View(licorViewModel);
        }

        [HttpPost]
        public ActionResult Create(LicoresViewModel licorViewModel)
        {
            byte[] img = null;
            using (var binary = new System.IO.BinaryReader(licorViewModel.Foto.InputStream))
            {
                img = binary.ReadBytes(licorViewModel.Foto.ContentLength);
            }
            string archivoBase64 = System.Convert.ToBase64String(img);
            licorViewModel.Foto_Licor = archivoBase64;

            using (var binary = new System.IO.BinaryReader(licorViewModel.Foto_Det.InputStream))
            {
                img = binary.ReadBytes(licorViewModel.Foto_Det.ContentLength);
            }
            archivoBase64 = System.Convert.ToBase64String(img);
            licorViewModel.Foto_Detalles = archivoBase64;
            using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                unidad.genericDAL.Add(this.Convertir(licorViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        [NonAction]
        private string Convertir_Imagen(string ruta)
        {
            byte[] archivoBytes = System.IO.File.ReadAllBytes(ruta);
            string archivoBase64 = System.Convert.ToBase64String(archivoBytes);
            return archivoBase64;
        }

        public ActionResult Edit(int id)
        {

            Licores licor;
            using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                licor = unidad.genericDAL.Get(id);
            }

            LicoresViewModel licorViewModel = this.Convertir(licor);

            using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                licorViewModel.marcas = Unidad.genericDAL.GetAll();
            }

            using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                licorViewModel.tipos = Unidad.genericDAL.GetAll();
            }

            using (UnidadDeTrabajo<Proveedores> Unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                licorViewModel.proveedores = Unidad.genericDAL.GetAll();
            }

            return View(licorViewModel);
        }

        [HttpPost]
        public ActionResult Edit(LicoresViewModel licorViewModel)
        {
            if (licorViewModel.Foto != null)
            {
                byte[] img = null;
                using (var binary = new System.IO.BinaryReader(licorViewModel.Foto.InputStream))
                {
                    img = binary.ReadBytes(licorViewModel.Foto.ContentLength); 
                }
                string archivoBase64 = System.Convert.ToBase64String(img);
                licorViewModel.Foto_Licor = archivoBase64;
            }
            if (licorViewModel.Foto_Det != null)
            {
                byte[] img = null;
                using (var binary = new System.IO.BinaryReader(licorViewModel.Foto_Det.InputStream))
                {
                    img = binary.ReadBytes(licorViewModel.Foto_Det.ContentLength);
                }
                string archivoBase64 = System.Convert.ToBase64String(img);
                licorViewModel.Foto_Detalles = archivoBase64;
            }

            using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(licorViewModel));
                unidad.Complete();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
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

        public ActionResult Delete(int id)
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

        [HttpPost]
        public ActionResult Delete(LicoresViewModel licorViewModel)
        {
            using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(licorViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }
    }
}