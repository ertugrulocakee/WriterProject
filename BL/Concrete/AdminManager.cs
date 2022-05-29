using BL.Abstract;
using DAL.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Concrete
{
    public class AdminManager : IAdminService
    {

        IAdminDAL adminDAL;

        public AdminManager(IAdminDAL adminDAL)
        {
            this.adminDAL = adminDAL;
        }

        public void TAdd(Admin t)
        {
            adminDAL.Add(t);
        }

        public void TDelete(Admin t)
        {
            adminDAL.Delete(t);
        }

        public List<Admin> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Admin TGetByID(int id)
        {
            return adminDAL.GetByID(id);
        }

        public List<Admin> TGetList()
        {
            return adminDAL.List();
        }

        public void TUpdate(Admin t)
        {
            adminDAL.Update(t);
        }
    }
}
