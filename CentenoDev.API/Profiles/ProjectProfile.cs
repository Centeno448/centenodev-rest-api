using AutoMapper;
using CentenoDev.API;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Project;
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

            CreateMap<ProjectForCreation, ProjectEntity>();

            CreateMap<ProjectForUpdate, ProjectEntity>();

            CreateMap<ProjectEntity, ProjectForUpdate>();
        }
    }
}
