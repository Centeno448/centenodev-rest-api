using AutoMapper;
using CentenoDev.API.Entities;
using CentenoDev.API.Models.Lesson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentenoDev.API.Profiles
{
    public class LessonProfile : Profile
    {
        public LessonProfile()
        {
            CreateMap<LessonEntity, Lesson>();
        }
    }
}
