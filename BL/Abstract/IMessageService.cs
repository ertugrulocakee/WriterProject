using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {

        List<Message> GetSendBox(string email);
        List<Message> GetReceiveBox(string email);

    }
}
