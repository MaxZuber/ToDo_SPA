using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDo.Dal.Abstract;
using ToDo.Dal.Concrete;
using ToDo.Entities;

namespace ToDo.WebUI.Controllers
{
    public class TasksController : ApiController
    {

        private readonly ITaskRepository _taskRepository;



        public TasksController(ITaskRepository taskRepository)
        {

            //string connectionString = ConfigurationManager.ConnectionStrings["TodoEntities_WebDeploy"].ConnectionString;

            _taskRepository = taskRepository;
        }

        [HttpGet]
        public List<tblTasks> Get()
        {
            List<tblTasks> tasks = this._taskRepository.GetAll();

            tasks.ForEach(n => n.tblUsers = null);

            return tasks;
        }

        [HttpPost]
        public tblTasks Post(tblTasks task)
        {

            task.DueDate = DateTime.Now;
           return this._taskRepository.Insert(task);

        }

        [HttpDelete]
        public void Delete(int id)
        {
            this._taskRepository.Delete(id);
        }

        [HttpPut]
        public void Update (tblTasks task)
        {
            this._taskRepository.Update(task);
        }

    }
}
