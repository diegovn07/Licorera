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
    public class RolesController : Controller
    {
        private RolesViewModel Convertir(Roles rol)
        {
            RolesViewModel rolViewModel = new RolesViewModel
            {
                RoleId = rol.RoleId,
                NombreRol = rol.NombreRol
            };
            return rolViewModel;
        }

        private Roles Convertir(RolesViewModel rolViewModel)
        {
            Roles rol = new Roles
            {
                RoleId = rolViewModel.RoleId,
                NombreRol = rolViewModel.NombreRol
            };
            return rol;
        }
        // GET: Roles
        public ActionResult Index()
        {
            List<Roles> roles;
            using (UnidadDeTrabajo<Roles> Unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                roles = Unidad.genericDAL.GetAll().ToList();
            }

            List<RolesViewModel> lista = new List<RolesViewModel>();

            foreach (var item in roles)
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
        public ActionResult Create(RolesViewModel rolViewModel)
        {
            Roles rol = this.Convertir(rolViewModel);

            using (UnidadDeTrabajo<Roles> unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                unidad.genericDAL.Add(rol);
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            Roles rol;
            using (UnidadDeTrabajo<Roles> unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                rol = unidad.genericDAL.Get(id);

            }


            return View(this.Convertir(rol));
        }

        [HttpPost]
        public ActionResult Edit(RolesViewModel rolViewModel)
        {


            using (UnidadDeTrabajo<Roles> unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                unidad.genericDAL.Update(this.Convertir(rolViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {

            Roles rol;
            using (UnidadDeTrabajo<Roles> unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                rol = unidad.genericDAL.Get(id);

            }

            return View(this.Convertir(rol));
        }

        [HttpPost]
        public ActionResult Delete(RolesViewModel rolViewModel)
        {
            using (UnidadDeTrabajo<Roles> unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                unidad.genericDAL.Remove(this.Convertir(rolViewModel));
                unidad.Complete();
            }

            return RedirectToAction("Index");
        }






    }
}