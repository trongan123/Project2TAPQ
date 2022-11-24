using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.IService
{
    public interface IAccountService
    {
        List<Account> getAll();
        Account FindAccountById(string id);
        void AddAccount(Account a);
        List<Account> getAllAccountByStatus(int st);
        List<Account> getAllAccountStaffFarm(string IdFarm);

        List<Account> getAllAccountByRole(int ro);
        void UnBlockAccount(Account a);
        List<Account> SearchAccountByPhone(String phone);
        Account Login(string user, string password);
        void UpdateAccount(Account a);

        void DeleteAccount(Account a);

    }
}
