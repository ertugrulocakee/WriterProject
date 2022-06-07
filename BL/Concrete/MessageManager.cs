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
    public class MessageManager : IMessageService
    {

        IMessageDAL _messageDAL;

        public MessageManager(IMessageDAL messageDAL)
        {
            _messageDAL = messageDAL;
        }

        public List<Message> GetReceiveBox(string email)
        {
            return _messageDAL.FilterList(m => m.ReceiverMail == email);    
        }

        public List<Message> GetSendBox(string email)
        {
            return _messageDAL.FilterList(m => m.SenderMail == email);
        }

        public void TAdd(Message t)
        {
            _messageDAL.Add(t);
        }

        public void TDelete(Message t)
        {
            _messageDAL.Delete(t);  
        }

        public List<Message> TFilterList()
        {
            throw new NotImplementedException();
        }

        public Message TGetByID(int id)
        {
           return _messageDAL.GetByID(id);  
        }

        public List<Message> TGetList()
        {
            return _messageDAL.List();
        }

        public void TUpdate(Message t)
        {
            _messageDAL.Update(t);
        }
    }
}
