using AutoMapper;
using Board.Contracts.FavoriteAd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Contracts.FavoriteAd;
using UpBoard.Domain;

namespace Doska.AppServices.MapProfile
{
    public class FavoriteAdMapProfile : Profile
    {
        public FavoriteAdMapProfile()
        {
            CreateMap<FavoriteAd, CreateFavoriteAdRequest>().ReverseMap();
            CreateMap<FavoriteAd, InfoFavoriteAdResponse>().ReverseMap();
            CreateMap<FavoriteAd, DeleteFavoriteAdRequest>().ReverseMap();
        }
    }
}
