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
    public class ProveedoresController : Controller
    {
        private ProveedoresViewModel Convertir(Proveedores proveedor)
        {
            ProveedoresViewModel proveedorViewModel = new ProveedoresViewModel
            {
                idProveedor = proveedor.idProveedor,
                vNombre = proveedor.vNombre,
                iCedula = proveedor.iCedula,
                iTelefono = proveedor.iTelefono,
                vCorreo = proveedor.vCorreo,
                vDireccion = proveedor.vDireccion,
            };
            return proveedorViewModel;
        }

        private Proveedores Convertir(ProveedoresViewModel proveedorViewModel)
        {
            Proveedores proveedor = new Proveedores
            {
                idProveedor = proveedorViewModel.idProveedor,
                vNombre = proveedorViewModel.vNombre,
                iCedula = proveedorViewModel.iCedula,
                iTelefono = proveedorViewModel.iTelefono,
                vCorreo = proveedorViewModel.vCorreo,
                vDireccion = proveedorViewModel.vDireccion,
            };
            return proveedor;
        }

        // GET: Proveedores
        public ActionResult Index()
        {
            List<Proveedores> proveedor;
            using (UnidadDeTrabajo<Proveedores> Unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                proveedor = Unidad.genericDAL.GetAll().ToList();
            }

            List<ProveedoresViewModel> lista = new List<ProveedoresViewModel>();

            foreach (var item in proveedor)
            {
                lista.Add(this.Convertir(item));
            }
            return View(lista);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProveedoresViewModel proveedorViewModel)
        {
            Proveedores proveedor = this.Convertir(proveedorViewModel);

            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                unidad.genericDAL.Add(proveedor);
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            Proveedores proveedor;
            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                proveedor = unidad.genericDAL.Get(id);

            }


            return View(this.Convertir(proveedor));
        }

        [HttpPost]
        public ActionResult Edit(ProveedoresViewModel proveedorViewModel)
        {


            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(proveedorViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            Proveedores proveedor;
            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                proveedor = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(proveedor));
        }

        public ActionResult Delete(int id)
        {

            Proveedores proveedor;
            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                proveedor = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(proveedor));
        }

        [HttpPost]
        public ActionResult Delete(ProveedoresViewModel proveedorViewModel)
        {
            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(proveedorViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }
    }
}