using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BackEnd.DAL;
using BackEnd.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBackEnd
{

    [TestClass]
    public class TestRolDAL
    {
        private UnidadDeTrabajo<Roles> unidad;
        [TestMethod]
        public void TestAddRol()
        {

            Roles rol = new Roles
            {
                RoleId = 3,
                NombreRol = "Rol_prueba"

            };
            using (unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                unidad.genericDAL.Add(rol);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateRol()
        {
            using (unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                Roles rol = unidad.genericDAL.Get(4);
                rol.NombreRol = "Test Rol";
                unidad.genericDAL.Update(rol);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDeleteRol()
        {
            Roles rol = new Roles
            {
                RoleId = 4
            };
            using (unidad = new UnidadDeTrabajo<Roles>(new BDContext()))
            {
                unidad.genericDAL.Remove(rol);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestGetByIdRol()
        {

            using (UnidadDeTrabajo<Roles> unidad = new UnidadDeTrabajo<Roles>(new BDContext()))

            {
                Roles rol = unidad.genericDAL.Get(1);
                Assert.AreEqual(true, unidad.Complete());
            }


        }
    }
}
