using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IGenericDAL<T> where T : class 
    {

        void Add(T t);
        void Delete(T t);
        void Update(T t);
        List<T> List();
        T GetByID(int id);
        List<T> FilterList(Expression<Func<T, bool>> filter);


    }
}
