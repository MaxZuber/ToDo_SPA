using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Dal.Abstract;
using ToDo.Entities;

namespace ToDo.Dal.Concrete
{
    public class UserRepository :AbstractRepository, IUserRepository
    {

        public UserRepository(string connetctionString) : base(connetctionString) { }
        public tblUsers Register(tblUsers user)
        {
            using(var context = this.CreateDbContext())
            {
                bool isUserExist =  context.Set<tblUsers>().Any(n => n.Username == user.Username);

                if(isUserExist)
                {
                    user = null;
                }
                else
                {
                    context.Set<tblUsers>().Add(user);
                    context.SaveChanges();
                }
            }

            return user;
        }

        public tblUsers Login(string username, string password)
        {
            tblUsers retUser;

            using(var context = this.CreateDbContext())
            {
                retUser = context.Set<tblUsers>().SingleOrDefault(n => n.Username == username && n.Password == password);
            }
            return retUser;
                 
        }
    }
}
