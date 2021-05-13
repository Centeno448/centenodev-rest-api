using CentenoDev.API.Data;
using CentenoDev.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Services
{
    public class AccountService : IAccountService
    {
        private CentenoDevDBContext _context;

        public AccountService(CentenoDevDBContext context)
        {
            _context = context;
        }

        public async Task<AccountEntity> LoginUser(AccountEntity account)
        {
            return await _context.Account.Where(a => a.Username == account.Username && a.Password == account.Password).FirstOrDefaultAsync();
        }

        public async void CreateUser(AccountEntity account)
        {
            account.AccountGuid = Guid.NewGuid();

            await _context.Account.AddAsync(account);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}
