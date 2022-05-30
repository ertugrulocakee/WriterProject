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
    public class SkillManager : ISkillService
    {

        ISkillDAL skillDAL;

        public SkillManager(ISkillDAL skillDAL)
        {
            this.skillDAL = skillDAL;
        }

        public void TAdd(Skill t)
        {
            skillDAL.Add(t);
        }

        public void TDelete(Skill t)
        {
            skillDAL.Delete(t);
        }

        public List<Skill> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Skill TGetByID(int id)
        {
            return skillDAL.GetByID(id);
        }

        public List<Skill> TGetList()
        {
            return skillDAL.List();
        }

        public void TUpdate(Skill t)
        {
            skillDAL.Update(t);
        }
    }
}
