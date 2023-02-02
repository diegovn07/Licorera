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
    [Authorize(Roles ="Administrador")]
    public class MarcasController : Controller
    {

        private MarcasViewModel Convertir(Marcas marca)
        {
            MarcasViewModel marcasViewModel = new MarcasViewModel
            {
                idMarca = marca.idMarca,
                vNombre = marca.vNombre
            };
            return marcasViewModel;
        }

        private Marcas Convertir(MarcasViewModel marcasViewModel)
        {
            Marcas marca = new Marcas
            {
                idMarca = marcasViewModel.idMarca,
                vNombre = marcasViewModel.vNombre
            };
            return marca;
        }

        // GET: Marcas
       // [Authorize]
        public ActionResult Index()
        {
            List<Marcas> marcas;
            using (UnidadDeTrabajo<Marcas> Unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                marcas = Unidad.genericDAL.GetAll().ToList();
            }

            List<MarcasViewModel> lista = new List<MarcasViewModel>();

            foreach (var item in marcas)
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
        public ActionResult Create(MarcasViewModel marcasViewModel)
        {
            Marcas marcas = this.Convertir(marcasViewModel);

            using (UnidadDeTrabajo<Marcas> unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                unidad.genericDAL.Add(marcas);
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            Marcas marcas;
            using (UnidadDeTrabajo<Marcas> unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                marcas = unidad.genericDAL.Get(id);

            }


            return View(this.Convertir(marcas));
        }

        [HttpPost]
        public ActionResult Edit(MarcasViewModel marcasViewModel)
        {


            using (UnidadDeTrabajo<Marcas> unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(marcasViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            Marcas marcas;
            using (UnidadDeTrabajo<Marcas> unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                marcas = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(marcas));
        }

        public ActionResult Delete(int id)
        {

            Marcas marcas;
            using (UnidadDeTrabajo<Marcas> unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                marcas = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(marcas));
        }

        [HttpPost]
        public ActionResult Delete(MarcasViewModel marcasViewModel)
        {
            using (UnidadDeTrabajo<Marcas> unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(marcasViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }
    }
}