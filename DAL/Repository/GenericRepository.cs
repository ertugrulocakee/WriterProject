using DAL.Abstract;
using DAL.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        Context c = new Context();

        DbSet<T> _object;

        public GenericRepository()
        {
            _object = c.Set<T>();

        }

        public void Add(T t)
        {
            _object.Add(t);
            c.SaveChanges();

        }

        public void Delete(T t)
        {
            _object.Remove(t);
            c.SaveChanges();

        }

        public T GetByID(int id)
        {

            return _object.Find(id);

        }

        public List<T> List()
        {
            return _object.ToList();

        }

        public List<T> FilterList(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            return _object.Where(filter).ToList();  
        }

        public void Update(T t)
        {
            c.SaveChanges();
        }
    }
}
