using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Dal.Abstract;
using ToDo.Entities;

namespace ToDo.Dal.Concrete
{
    public class TaskRepository : AbstractRepository, ITaskRepository
    {

        public TaskRepository(string connectionString) : base(connectionString) { }

        public List<Entities.tblTasks> GetAll()
        {

            List<tblTasks> tasks;
            using (DbContext context = this.CreateDbContext())
            {
                tasks = context.Set<tblTasks>()
                    .Include(n => n.tblUsers)
                    .ToList();
            }
            return tasks;

        }
        public List<tblTasks> GetTasksForUserID(int ID)
        {
            List<tblTasks> tasks;
            using (DbContext context = this.CreateDbContext())
            {
                tasks = context.Set<tblTasks>()
                    .Where(n => n.UserID == ID)
                    .Include(n => n.tblUsers)
                    .ToList();
            }
            return tasks;
        }

        public Entities.tblTasks Insert(Entities.tblTasks task)
        {

            using (DbContext context = this.CreateDbContext())
            {
                task.UserID = 1;
                context.Set<tblTasks>().Add(task);
                context.SaveChanges();
            }

            return task;


        }

        public void Delete(int id)
        {
            using (DbContext context = this.CreateDbContext())
            {
                tblTasks task = new tblTasks() { ID = id };
                context.Set<tblTasks>().Attach(task);
                context.Set<tblTasks>().Remove(task);
                context.SaveChanges();
            }
        }


        public void Update(tblTasks task)
        {
            using(var context = this.CreateDbContext())
            {
                tblTasks oldTask = context.Set<tblTasks>().First(n => n.ID == task.ID);
                context.Entry(oldTask).CurrentValues.SetValues(task);
                context.SaveChanges();
            }
        }


        public List<tblTasks> GetTasksForUserID(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
