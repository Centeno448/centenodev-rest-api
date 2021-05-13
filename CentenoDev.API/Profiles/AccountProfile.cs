using AutoMapper;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CentenoDev.API.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountEntity>()
                .ForMember(
                    dest => dest.Password, opt => 
                    opt.MapFrom(src => 
                    Encoding.ASCII.GetString(
                        SHA256.Create().ComputeHash(
                            Encoding.ASCII.GetBytes(src.Password)))
                    ));
        }
    }
}