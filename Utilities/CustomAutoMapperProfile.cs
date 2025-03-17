using AutoMapper;
using MyModels;
using MyModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class CustomAutoMapperProfile:Profile
    {
        public CustomAutoMapperProfile()
        {
            base.CreateMap<WriterInfo,WriterDTO>();
            base.CreateMap<BlogNews,BlogNewsDTO>()
                .ForMember(d=>d.TypeName,s=>s.MapFrom(ss=>ss.TypeInfo.Name))
                .ForMember(d=>d.WriterName,s=>s.MapFrom(ss=>ss.WriterInfo.Name));
        }
    }
}
