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
    public class TestUserDAL
    {
        private UnidadDeTrabajo<Users> unidad;
        [TestMethod]
        public void TestAddUser()
        {
            Users user = new Users
            {
                UserId = 3,
                UserName = "Prueba",
                cedula = "123232290",
                nombre = "Prueba",
                apellido1 = "Apellido prueba",
                apellido2 = "Apellido prueba2",
                telefono = "78562451",
                telefono2 = "78562451",
                Correo = "prueba@usuario.com",
                Password = "Contra_Prueba",
                direccion = "Direccion_prueba"
            };
            using (unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                unidad.genericDAL.Add(user);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateUser()
        {
            using (unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                Users user = unidad.genericDAL.Get(4);
                user.UserId = 4;
                user.UserName = "Prueba";
                user.cedula = "123232290";
                user.nombre = "Prueba";
                user.apellido1 = "Apellido prueba";
                user.apellido2 = "Apellido prueba2";
                user.telefono = "78562451";
                user.telefono2 = "78562451";
                user.Correo = "prueba@usuario.com";
                user.Password = "Contra_Prueba";
                user.direccion = "Direccion_prueba";
                unidad.genericDAL.Update(user);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDeleteUser()
        {
            Users user = new Users
            {
                UserId = 4
            };
            using (unidad = new UnidadDeTrabajo<Users>(new BDContext()))
            {
                unidad.genericDAL.Remove(user);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestGetByIdUser()
        {

            using (UnidadDeTrabajo<Users> unidad = new UnidadDeTrabajo<Users>(new BDContext()))

            {
                Users user = unidad.genericDAL.Get(1);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

    }
}
