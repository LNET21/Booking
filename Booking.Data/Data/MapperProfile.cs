﻿using AutoMapper;
using Booking.Core.Models.Entities;
using Booking.Core.Models.ViewModels.GymClasses;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<GymClass, GymClassesViewModel>()
            //    .ForMember(dest => dest.Attending, opt => opt.Ignore());

            //CreateMap<GymClass, GymClassesViewModel>()
            //   .ForMember(dest => dest.Attending, from => from.MapFrom(
            //       (src, dest, _, context) => src.AttendingMembers.Any(a => a.ApplicationUserId == context.Items["Id"].ToString()))); 

            CreateMap<GymClass, GymClassesViewModel>()
               .ForMember(dest => dest.Attending, from => from.MapFrom<AttendingResolver>());
              
        }
    }

    public class AttendingResolver : IValueResolver<GymClass, GymClassesViewModel, bool>
    {
        //private readonly IHttpContextAccessor httpContextAccessor;

        //public AttendingResolver(IHttpContextAccessor httpContextAccessor)
        //{
        //    this.httpContextAccessor = httpContextAccessor;
        //}

        public bool Resolve(GymClass source, GymClassesViewModel destination, bool destMember, ResolutionContext context)
        {
            if (source.AttendingMembers == null || context.Items.Count == 0) return false;
            return source.AttendingMembers.Any(a => a.ApplicationUserId == context.Items["Id"].ToString());

            //From Http Context
            //return source.AttendingMembers is null ?  false : 
            //      source.AttendingMembers.Any(a => a.ApplicationUserId == httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
