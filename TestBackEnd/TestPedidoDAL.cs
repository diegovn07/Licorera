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
    public class TestPedidoDAL
    {
        private UnidadDeTrabajo<Pedidos> unidad;

        [TestMethod]
        public void TestAddPedido()
        {

            Pedidos pedido = new Pedidos
            {
                idPedido = 5,
                vNombre_Cliente = "Cliente Prueba",
                vTelefono = "82483627",
                vEmail = "prueba@gmail.com",
                vDireccion_Envio = "Direccion Prueba",
                dFecha = DateTime.Now,
                idEstado_Pedido = 1,
                idMedio_Pago = 1,
                bEstado_Pago = false,
                identificacion = "11248347"
                
            };
            using (unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                unidad.genericDAL.Add(pedido);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

        [TestMethod]
        public void TestUpdatePedido()
        {
            using (unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                Pedidos pedido = unidad.genericDAL.Get(14);
                pedido.vNombre_Cliente = "Cliente Prueba";
                pedido.vTelefono = "82483627";
                pedido.vEmail = "prueba@gmail.com";
                pedido.vDireccion_Envio = "Direccion Prueba";
                pedido.dFecha = DateTime.Now;
                pedido.idEstado_Pedido = 1;
                pedido.idMedio_Pago = 2;
                pedido.bEstado_Pago = false;
                pedido.identificacion = "11248347";
                unidad.genericDAL.Update(pedido);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestDeletePedido()
        {
            Pedidos pedidos = new Pedidos
            {
                idPedido = 21
            };
            using (unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))
            {
                unidad.genericDAL.Remove(pedidos);
                Assert.AreEqual(true, unidad.Complete());
            }
        }

        [TestMethod]
        public void TestGetByIdPedido()
        {

            using (UnidadDeTrabajo<Pedidos> unidad = new UnidadDeTrabajo<Pedidos>(new BDContext()))

            {
                Pedidos pedido = unidad.genericDAL.Get(15);
                Assert.AreEqual(true, unidad.Complete());
            }

        }

    }

}

