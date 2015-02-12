using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Entities
{
    public class tblTasks
    {
        public int ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public int Status { set; get; }
        public DateTime? DueDate { set; get; }
    }
}
