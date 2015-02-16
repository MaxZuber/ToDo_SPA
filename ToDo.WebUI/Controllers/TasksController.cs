using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using ToDo.Dal.Abstract;
using ToDo.Dal.Concrete;
using ToDo.Entities;

namespace ToDo.WebUI.Controllers
{
    [Authorize]
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

            ClaimsPrincipal user = User as ClaimsPrincipal;
            string userID = user.Claims.Single(n => n.Type == ClaimTypes.NameIdentifier).Value;

            List<tblTasks> tasks = this._taskRepository.GetTasksForUserID(int.Parse(userID));

            tasks.ForEach(n => n.tblUsers = null);

            return tasks;
        }
        [HttpGet]
        public List<tblTasks> Get(int id)
        {
            List<tblTasks> tasks = this._taskRepository.GetTasksForUserID(id);

            tasks.ForEach(n => n.tblUsers = null);

            return tasks;
        }

        [HttpPost]
        public tblTasks Post(tblTasks task)
        {
            ClaimsPrincipal user = User as ClaimsPrincipal;
            string userID = user.Claims.Single(n => n.Type == ClaimTypes.NameIdentifier).Value;
            task.DueDate = DateTime.Now;
            task.UserID = int.Parse(userID);
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
