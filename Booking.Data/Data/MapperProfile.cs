using AutoMapper;
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

            CreateMap<IEnumerable<GymClass>, IndexViewModel>()
                .ForMember(dest => dest.ShowHistory, opt => opt.Ignore())
                .ForMember(dest => dest.GymClasses, from => from.MapFrom(g => g.ToList()));
              
        }
    }

    //public class AttendingResolver : IValueResolver<GymClass, GymClassesViewModel, bool>
    //{
    //    public bool Resolve(GymClass source, GymClassesViewModel destination, bool destMember, ResolutionContext context)
    //    {
    //       return source.AttendingMembers != null && context.Items.TryGetValue("Id", out object key)
    //            && (key is string id && source.AttendingMembers.Any(a => a.ApplicationUserId == id));
    //    }
    //} 
    
    public class AttendingResolver : IValueResolver<GymClass, GymClassesViewModel, bool>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AttendingResolver(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public bool Resolve(GymClass source, GymClassesViewModel destination, bool destMember, ResolutionContext context)
        {

            return source.AttendingMembers is null ? false :
                  source.AttendingMembers.Any(a => a.ApplicationUserId == httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
