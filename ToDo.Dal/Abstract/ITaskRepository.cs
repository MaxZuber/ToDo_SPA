using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Dal.Abstract
{
    public interface ITaskRepository
    {
        List<tblTasks> GetAll();
        List<tblTasks> GetTasksForUserID(int ID);
        tblTasks Insert(tblTasks task);
        void Delete(int id);

        void Update(tblTasks task);

    }
}
