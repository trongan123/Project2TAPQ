using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IRoleStaffService
    {
        List<RoleStaff> getAll();
        RoleStaff FindRoleStaffById(string id);

        string Getid(string con);
        void AddRoleStaff(RoleStaff a);

        void UpdateRoleStaff(RoleStaff a);

        void DeleteRoleStaff(RoleStaff a);

    }
}
