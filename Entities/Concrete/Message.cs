using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Message
    {

       [Key]
       public int MessageID { get; set; }

       public string SenderMail { get; set; }

       public string ReceiverMail { get; set; }  

       public string Title { get; set; }

       public string MessageDescription { get; set; }   

       public DateTime Date { get; set; }   

       public bool MessageStatus { get; set; }
       


    }
}
