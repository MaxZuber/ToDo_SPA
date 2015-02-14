using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using ToDo.Dal.Abstract;

namespace ToDo.WebUI.Services
{
    public interface ISecurityService
    {
        //IUserRepository UserRepository { set; get; }
        bool Login(string Username, string password, bool isPersistent);
        void Login();
        void Logout();
    }
}
