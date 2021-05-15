using CentenoDev.API.Entities;
using System.Threading.Tasks;

namespace CentenoDev.API.Services
{
    public interface IAccountService
    {
        Task<AccountEntity> LoginUser(AccountEntity account);

        Task CreateUser(AccountEntity account);

        Task<bool> SaveChangesAsync();
    }
}