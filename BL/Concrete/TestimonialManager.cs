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
    public class TestimonialManager : ITestimonialService
    {

        ITestimonialDAL _testimonialDAL;

        public TestimonialManager(ITestimonialDAL testimonialDAL)
        {
            _testimonialDAL = testimonialDAL;
        }

        public Testimonial GetTestimonialByWriter(int writerId)
        {
            return _testimonialDAL.FilterList(m => m.WriterID == writerId && m.TestimonialStatus == true).FirstOrDefault();
        }

        public void TAdd(Testimonial t)
        {
            _testimonialDAL.Add(t);
        }

        public void TDelete(Testimonial t)
        {
            _testimonialDAL.Delete(t);
        }

        public List<Testimonial> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Testimonial TGetByID(int id)
        {
            return _testimonialDAL.GetByID(id);
        }

        public List<Testimonial> TGetList()
        {
            return _testimonialDAL.List();
        }

        public void TUpdate(Testimonial t)
        {
            _testimonialDAL.Update(t);
        }
    }
}
