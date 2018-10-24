using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IRepository<T> where T : class
    {
        bool Save(T entity);
        bool Modify(T entity);
        bool Delete(int id);
        T Search(int id);
        List<T> GetList(Expression<Func<T, bool>> expression);

    }
}
