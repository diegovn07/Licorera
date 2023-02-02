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
    public class TestLicorDAL
    {
        private UnidadDeTrabajo<Licores> unidad;

        [TestMethod]
        public void TestAddLicor()
        {

            Licores licor = new Licores
            {
                idLicor = 6,
                idMarca = 3,
                idTipo = 1,
                idProveedor = 1,
                vDescripción = "Licor Prueba",
                iUnidades = 10,
                iPrecio = 1000,
                Foto_Licor = ".",
                iMl = 20,
                Foto_Detalles = "."
            };
            using (unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                unidad.genericDAL.Add(licor);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateLicor()
        {
            using (unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                Licores licor = unidad.genericDAL.Get(4);
                licor.idMarca = 4;
                licor.idTipo = 1;
                licor.idProveedor = 1;
                licor.vDescripción = "Licor Prueba";
                licor.iUnidades = 10;
                licor.iPrecio = 1000;
                licor.Foto_Licor = ".";
                licor.iMl = 20;
                unidad.genericDAL.Update(licor);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDeleteLicor()
        {
            Licores licor = new Licores
            {
                idLicor = 9
            };
            using (unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            {
                unidad.genericDAL.Remove(licor);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestGetByIdLicor()
        {

            using (UnidadDeTrabajo<Licores> unidad = new UnidadDeTrabajo<Licores>(new BDContext()))
            
            {
               Licores licor = unidad.genericDAL.Get(1);
               Assert.AreEqual(true, unidad.Complete());
            }           

        }

    }
}
