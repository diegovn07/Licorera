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
    public class TestProveedorDAL
    {
        private UnidadDeTrabajo<Proveedores> unidad;
        [TestMethod]
        public void TestAddProveedor()
        {

            Proveedores proveedor = new Proveedores
            {
                idProveedor = 2,
                vNombre = "Proveedor Prueba",
                iCedula = 82483627,
                iTelefono = 22164827,
                vCorreo = "proveeodr@gmail.com",
                vDireccion = "Direccicon proveedor, prueba"                

            };
            using (unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                unidad.genericDAL.Add(proveedor);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdateProveedor()
        {
            using (unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                Proveedores proveedor = unidad.genericDAL.Get(3);
                proveedor.vNombre = "Test proveedor";
                unidad.genericDAL.Update(proveedor);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDeleteProveedor()
        {
            Proveedores proveedor = new Proveedores
            {
                idProveedor = 3
            };
            using (unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))
            {
                unidad.genericDAL.Remove(proveedor);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestGetByIdProveedor()
        {

            using (UnidadDeTrabajo<Proveedores> unidad = new UnidadDeTrabajo<Proveedores>(new BDContext()))

            {
                Proveedores proveedor = unidad.genericDAL.Get(1);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

    }
}
