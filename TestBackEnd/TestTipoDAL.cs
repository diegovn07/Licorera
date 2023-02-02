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
    public class TestTipoDAL
    {
        private UnidadDeTrabajo<Tipos> unidad;

        [TestMethod]
        public void TestAddTipo()
        {

            Tipos tipo = new Tipos
            {
                idTipo = 3,
                vNombre = "Tipo_prueba"

            };
            using (unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                unidad.genericDAL.Add(tipo);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateTipo()
        {
            using (unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                Tipos tipo = unidad.genericDAL.Get(4);
                tipo.vNombre = "Test Tipo";
                unidad.genericDAL.Update(tipo);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDeleteTipo()
        {
            Tipos tipo = new Tipos
            {
                idTipo = 4
            };
            using (unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                unidad.genericDAL.Remove(tipo);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestGetByNameTipo()
        {

            using (unidad = new UnidadDeTrabajo<Tipos>(new BDContext()))
            {
                Expression<Func<Tipos, bool>> consulta = (c => c.vNombre.Contains("Tequila"));
                List<Tipos> lista = unidad.genericDAL.Find(consulta).ToList();

                Assert.AreEqual(true, unidad.Complete());
            }


        }

    }
}
