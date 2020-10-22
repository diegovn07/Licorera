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
    public class TiposController : Controller
    {
        private TiposViewModel Convertir(Tipos tipo)
        {
            TiposViewModel tiposViewModel = new TiposViewModel
            {
                idTipo = tipo.idTipo,
                vNombre = tipo.vNombre
            };
            return tiposViewModel;
        }

        private Tipos Convertir(TiposViewModel tiposViewModel)
        {
            Tipos tipo = new Tipos
            {
                idTipo = tiposViewModel.idTipo,
                vNombre = tiposViewModel.vNombre
            };
            return tipo;
        }

        // GET: Tipos
        public ActionResult Index()
        {
            List<Tipos> tipos;
            using (UnidadDeTrabajo<Tipos> Unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                tipos = Unidad.genericDAL.GetAll().ToList();
            }

            List<TiposViewModel> lista = new List<TiposViewModel>();

            foreach (var item in tipos)
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
        public ActionResult Create(TiposViewModel tiposViewModel)
        {
            Tipos tipos = this.Convertir(tiposViewModel);

            using (UnidadDeTrabajo<Tipos> unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                unidad.genericDAL.Add(tipos);
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            Tipos tipos;
            using (UnidadDeTrabajo<Tipos> unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                tipos = unidad.genericDAL.Get(id);

            }


            return View(this.Convertir(tipos));
        }

        [HttpPost]
        public ActionResult Edit(TiposViewModel tiposViewModel)
        {


            using (UnidadDeTrabajo<Tipos> unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(tiposViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {

            Tipos tipos;
            using (UnidadDeTrabajo<Tipos> unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                tipos = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(tipos));
        }

        public ActionResult Delete(int id)
        {

            Tipos tipos;
            using (UnidadDeTrabajo<Tipos> unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                tipos = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(tipos));
        }

        [HttpPost]
        public ActionResult Delete(TiposViewModel tiposViewModel)
        {
            using (UnidadDeTrabajo<Tipos> unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(tiposViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }
    }
}