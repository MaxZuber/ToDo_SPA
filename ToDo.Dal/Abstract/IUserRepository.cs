using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Dal.Abstract
{
    public interface IUserRepository
    {
        tblUsers Register(tblUsers user);
        tblUsers Login(string username, string password);

    }
}
