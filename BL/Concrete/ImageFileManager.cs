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
    public class ImageFileManager : IImageFileService
    {

        IImageFileDAL imageFileDAL;

        public ImageFileManager(IImageFileDAL imageFileDAL)
        {
            this.imageFileDAL = imageFileDAL;
        }

        public void TAdd(ImageFile t)
        {
            imageFileDAL.Add(t);
        }

        public void TDelete(ImageFile t)
        {
            imageFileDAL.Delete(t);
        }

        public List<ImageFile> TFilterList()
        {
            throw new NotImplementedException();
        }

        public ImageFile TGetByID(int id)
        {
            return imageFileDAL.GetByID(id);    
        }

        public List<ImageFile> TGetList()
        {
            return imageFileDAL.List();
        }

        public void TUpdate(ImageFile t)
        {
            imageFileDAL.Update(t);
        }
    }
}
