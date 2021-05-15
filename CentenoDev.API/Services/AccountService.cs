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
        private CentenoDevDBContext _db;

        public AccountService(CentenoDevDBContext context)
        {
            _db = context;
        }

        public async Task<AccountEntity> LoginUser(AccountEntity account)
        {
            return await _db.Account.Where(a => a.Username == account.Username && a.Password == account.Password).FirstOrDefaultAsync();
        }

        public async void CreateUser(AccountEntity account)
        {
            account.Guid = Guid.NewGuid();

            await _db.Account.AddAsync(account);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync() > 0);
        }
    }
}
