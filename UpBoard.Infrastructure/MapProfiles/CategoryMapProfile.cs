using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Board.Contracts.Category;
using UpBoard.Contracts.Category;
using UpBoard.Domain;

namespace Doska.AppServices.MapProfile
{
    public class CategoryMapProfile : Profile
    {
        public CategoryMapProfile()
        {
            CreateMap<Category,InfoCategoryResponse>().ReverseMap();
            CreateMap<CreateCategoryRequest, Category>().ReverseMap();
            CreateMap<CreateCategoryRequest,InfoCategoryResponse>().ReverseMap();
            CreateMap<UpdateCategoryRequest, Category>().ReverseMap();
            CreateMap<DeleteCategoryRequest, Category>().ReverseMap();
            CreateMap<DeleteCategoryRequest, InfoCategoryResponse>().ReverseMap();
            CreateMap<UpdateCategoryRequest, InfoCategoryResponse>().ReverseMap();
        }
    }
}
