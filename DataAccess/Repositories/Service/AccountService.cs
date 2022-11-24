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
    public class AccountService : IAccountService
    {
        public void AddAccount(Account a) => AccountDAO.AddAccount(a);

        public void DeleteAccount(Account a) => AccountDAO.DeleteAccount(a);

        public Account FindAccountById(string id) => AccountDAO.FindAccountById(id);

        public List<Account> getAll() => AccountDAO.getAll();

        public List<Account> getAllAccountByStatus(int st) => AccountDAO.getAllAccountByStatus(st);

        public List<Account> getAllAccountStaffFarm(string IdFarm) => AccountDAO.getAllAccountStaffFarm(IdFarm);
        public List<Account> getAllAccountByRole(int ro) => AccountDAO.getAllAccountByRole(ro);

        public Account Login(string user, string password) => AccountDAO.Login(user, password);

        public List<Account> SearchAccountByPhone(string phone) => AccountDAO.SearchAccountByPhone(phone);

        public void UnBlockAccount(Account a) => AccountDAO.UnBlockAccount(a);

        public void UpdateAccount(Account a) => AccountDAO.UpdateAccount(a);
    }
}
