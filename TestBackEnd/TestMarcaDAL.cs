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
    public class TestMarcaDAL
    {
        private UnidadDeTrabajo<Marcas> unidad;

        [TestMethod]
        public void TestAddMarca()
        {

            Marcas marca = new Marcas
            {
                vNombre = "Prueba"
            };
            using (unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                unidad.genericDAL.Add(marca);
                Assert.AreEqual(true, unidad.Complete());
            }

        }


        [TestMethod]
        public void TestGetByNameMarca()
        {
            using (unidad = new UnidadDeTrabajo<Marcas>(new BDContext()))
            {
                Expression<Func<Marcas, bool>> consulta = (c => c.vNombre.Contains("Jose Cuervo"));
                List<Marcas> lista = unidad.genericDAL.Find(consulta).ToList();

                Assert.AreEqual(true, unidad.Complete());
            }

        }
    }
}
