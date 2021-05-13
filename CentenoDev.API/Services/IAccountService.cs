using CentenoDev.API.Entities;
using System.Threading.Tasks;

namespace CentenoDev.API.Services
{
    public interface IAccountService
    {
        Task<AccountEntity> LoginUser(AccountEntity account);

        void CreateUser(AccountEntity account);

        Task<bool> SaveChangesAsync();
    }
}