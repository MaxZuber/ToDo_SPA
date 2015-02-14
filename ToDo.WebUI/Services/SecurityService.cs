using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using ToDo.Dal.Abstract;
using ToDo.Entities;

namespace ToDo.WebUI.Services
{
    public class SecurityService :ISecurityService
    {
        private IUserRepository _userRepository;

        [Dependency]
        public IUserRepository UserRepository
        {
            set { this._userRepository = value; }
            get { return this._userRepository; }
        }
        
        public SecurityService()
        {

        }

        public bool Login(string Username, string password, bool isPersistent)
        {
           tblUsers user =  _userRepository.Login(Username, password);

            if(user != null)
            {
                FormsAuthentication.SetAuthCookie(Username, isPersistent);
                HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(user.Username), null);
                return true;
            }
            else
            {
                HttpContext.Current.User = null;
                return false;
            }
        }

        public void Login()
        {

            IPrincipal currentUser = HttpContext.Current.User;

            if(currentUser != null && currentUser.Identity != null && currentUser.Identity.IsAuthenticated == true)
            {
                IIdentity identity = currentUser.Identity;
                HttpContext.Current.User = new GenericPrincipal(identity, null);

            }
            else
            {
                HttpContext.Current.User = null;
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}