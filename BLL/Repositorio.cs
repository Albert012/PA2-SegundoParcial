using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Repositorio<T> : IRepository<T> where T : class
    {
        public virtual bool Save(T entity)
        {
            bool step = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Set<T>().Add(entity) != null)
                {
                    contexto.SaveChanges();
                    step = true;
                }

            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }


            return step;
        }
        
        public virtual bool Modify(T entity)
        {
            bool step = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(entity).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    step = true;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return step;
        }

        public virtual bool Delete(int id)
        {
            bool step = false;
            Contexto contexto = new Contexto();

            try
            {
                T entity = contexto.Set<T>().Find(id);
                contexto.Set<T>().Remove(entity);
                if (contexto.SaveChanges() > 0)
                {
                    step = true;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }


            return step;
        }

        public virtual T Search(int id)
        {
            T entity = null;
            Contexto contexto = new Contexto();

            try
            {
                entity = contexto.Set<T>().Find(id);
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return entity;
        }

        public List<T> GetList(Expression<Func<T, bool>> expression)
        {
            List<T> list = new List<T>();
            Contexto contexto = new Contexto();

            try
            {
                list = contexto.Set<T>().Where(expression).ToList();
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return list;
        }

    }
}
