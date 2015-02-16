using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDo.WebUI.Services;

namespace ToDo.WebUI.Module
{
    public class AuthModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(this.Authenticate);
        }

        private void Authenticate(object source, EventArgs e)
        {
            //HttpApplication application = (HttpApplication)source;
            //HttpContext context = application.Context;

            ISecurityService securityService = DependencyResolver.Current.GetService<ISecurityService>();
            securityService.Login();
        }


        public void Dispose()
        {
        }
    }
}