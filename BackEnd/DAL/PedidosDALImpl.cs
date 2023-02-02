using BackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.DAL
{
    public class PedidosDALImpl : IDisposable
    {
        protected readonly BDContext Context;

        public PedidosDALImpl(BDContext context)
        {
            Context = context;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public int Add(Pedidos entity)
        {
            try
            {
                Context.Set<Pedidos>().Add(entity);
                Context.SaveChanges();
                return entity.idPedido;
            }
            catch (Exception e)
            {
                string msj = e.Message;
                return 0;
            }
        }

        public void AddRange(IEnumerable<Pedidos> entities)
        {
            try
            {
                Context.Set<Pedidos>().AddRange(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Pedidos> Find(Expression<Func<Pedidos, bool>> predicate)
        {
            try
            {
                return Context.Set<Pedidos>().Where(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Pedidos Get(int id)
        {
            try
            {
                return Context.Set<Pedidos>().Find(id);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public IEnumerable<Pedidos> GetAll()
        {
            try
            {
                return Context.Set<Pedidos>().ToList();
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Remove(Pedidos entity)
        {
            try
            {
                Context.Set<Pedidos>().Attach(entity);
                Context.Set<Pedidos>().Remove(entity);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public void RemoveRange(IEnumerable<Pedidos> entities)
        {
            try
            {
                Context.Set<Pedidos>().RemoveRange(entities);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Pedidos SingleOrDefault(Expression<Func<Pedidos, bool>> predicate)
        {
            try
            {
                return Context.Set<Pedidos>().SingleOrDefault(predicate);
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Update(Pedidos entity)
        {
            try
            {
                Context.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
