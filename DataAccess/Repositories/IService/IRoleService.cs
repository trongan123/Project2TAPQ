using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IRoleService
    {
        List<Role> getAll();
        Role FindRoleById(string id);
        void AddRole(Role a);

        void UpdateRole(Role a);

        void DeleteRole(Role a);
    }
}
