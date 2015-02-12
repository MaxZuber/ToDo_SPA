using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Dal.Abstract;
using ToDo.Entities;

namespace ToDo.Dal.Concrete
{
    public class FakeTaskRepository : ITaskRepository
    {

        private static List<tblTasks> _tasks;

        static FakeTaskRepository()
        {
            _tasks = CreateTasks();
        }

        public List<tblTasks> GetAll()
        {
            return _tasks;
        }
        private static List<tblTasks> CreateTasks()
        {
            return new List<tblTasks>()
            {
                new tblTasks()
                {
                    ID = 1,
                    Title = "Create Project",
                    Description = "DAL, Entities, WebUI",
                    Status = 1,
                    DueDate = DateTime.Now
                },
                new tblTasks()
                {
                    ID = 2,
                    Title = "Create TaskModel",
                    Description = "TaskModel in entities",
                    Status = 2,
                    DueDate = DateTime.Now
                },
                new tblTasks()
                {
                    ID = 3,
                    Title = "Create implement ITaskRepository",
                    Description = "FakeTaskRepository",
                    Status = 3,
                    DueDate = DateTime.Now
                },
                new tblTasks()
                {
                    ID = 4,
                    Title = "Wait new instructions",
                    Description = "...",
                    Status = 1,
                    DueDate = DateTime.Now
                }
            };
        }




        public tblTasks Insert(tblTasks task)
        {
            int MaxId = _tasks.Select(n => Convert.ToInt32(n.ID)).Max();
            task.ID = MaxId + 1;
            _tasks.Add(task);

            return task;
        }


        public void Delete(int id)
        {
            _tasks.RemoveAll(n => n.ID == id);
        }
    }
}
