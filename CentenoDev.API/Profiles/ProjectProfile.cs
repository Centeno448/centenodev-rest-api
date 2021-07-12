using AutoMapper;
using CentenoDev.API;
using CentenoDev.API.Entities;
using CentenoDev.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<ProjectEntity, Project>();
        }
    }
}
