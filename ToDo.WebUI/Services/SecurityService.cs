using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
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

            if(user == null)
            {
                return false;
            }

            FormsAuthentication.SetAuthCookie(Username, isPersistent);

            IPrincipal principal = this.CreatePrincipal(Username);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;

            }

            return true;
        }

        private IPrincipal CreatePrincipal(string Username)
        {
            tblUsers user = this._userRepository.Login(Username);
            ClaimsIdentity identity = new ClaimsIdentity("Password");
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            //identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            //identity.AddClaim(new Claim(ClaimTypes.Surname, user.Surname));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Mail));

            //if (user.Roles != null)
            //{
            //    foreach (string role in user.Roles)
            //    {
            //        identity.AddClaim(new Claim(ClaimTypes.Role, role));
            //    }
            //}
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            return principal;
        }

        public void Login()
        {

            IPrincipal oldPrincipal = HttpContext.Current.User;
            if (oldPrincipal == null)
            {
                return;
            }

            IPrincipal principal = this.CreatePrincipal(oldPrincipal.Identity.Name);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}