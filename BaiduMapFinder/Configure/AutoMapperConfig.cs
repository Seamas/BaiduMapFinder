using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BaiduMapFinder.ExportModels;

namespace BaiduMapFinder.Configure
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<BaiduMap.Response.Models.PlaceDetailItem, PlaceDetail>()
                .ForMember(item => item.Lat, opt => opt.MapFrom(src => src.Location.Lat))
                .ForMember(item => item.Lng, opt => opt.MapFrom(src => src.Location.Lng));
        }
    }
}
