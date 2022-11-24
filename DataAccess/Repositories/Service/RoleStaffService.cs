using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.Repositories.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Service
{
    public class RoleStaffService : IRoleStaffService
    {
        public void AddRoleStaff(RoleStaff a) => RoleStaffDAO.AddRoleStaff(a);

        public void DeleteRoleStaff(RoleStaff a) => RoleStaffDAO.DeleteRoleStaff(a);

        public RoleStaff FindRoleStaffById(string id) => RoleStaffDAO.FindRoleStaffById(id);

        public string Getid(string con) => RoleStaffDAO.Getid(con);

        public List<RoleStaff> getAll() => RoleStaffDAO.getAll();

        public void UpdateRoleStaff(RoleStaff a) => RoleStaffDAO.UpdateRoleStaff(a);
    }
}
