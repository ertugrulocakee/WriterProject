using BL.Abstract;
using DAL.Abstract;
using DAL.Repository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    public class CategoryManager : ICategoryService
    {

        ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public void TAdd(Category t)
        {
            _categoryDAL.Add(t);    
        }

        public void TDelete(Category t)
        {
            _categoryDAL.Delete(t);
        }

        public List<Category> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Category TGetByID(int id)
        {
            return _categoryDAL.GetByID(id);        
        }

        public List<Category> TGetList()
        {
            return _categoryDAL.List(); 
        }

        public void TUpdate(Category t)
        {
            _categoryDAL.Update(t);
        }
    }
}
