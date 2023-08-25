using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Board.Contracts.Ad;
using UpBoard.Contracts.Ad;
using UpBoard.Domain;

namespace Doska.AppServices.MapProfile
{
    public class AdvertisementMapProfile : Profile
    {
        public AdvertisementMapProfile()
        {
            CreateMap<Advertisement, InfoAdResponse>().ReverseMap();
            CreateMap<Advertisement, CreateAdvertisementRequest>().ReverseMap();
            CreateMap<Advertisement, DeleteAdRequest>().ReverseMap();
            CreateMap<Advertisement, UpdateAdRequest>().ReverseMap();
            CreateMap<InfoAdResponse, CreateAdvertisementRequest>().ReverseMap();
            CreateMap<InfoAdResponse, UpdateAdRequest>().ReverseMap();
            CreateMap<InfoAdResponse, DeleteAdRequest>().ReverseMap();

        }
    }
}
