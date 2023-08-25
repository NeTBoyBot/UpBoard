using AutoMapper;
using Board.Contracts.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Contracts.Comment;
using UpBoard.Domain;

namespace Doska.AppServices.MapProfile
{
    public class CommentMapProfile : Profile
    {
        public CommentMapProfile()
        {
            CreateMap<Comment, InfoCommentResponse>().ReverseMap();
            CreateMap<Comment, CreateCommentRequest>().ReverseMap();
            CreateMap<Comment, DeleteCommentRequest>().ReverseMap();
            CreateMap<Comment, UpdateCommentRequest>().ReverseMap();
        }
    }
}
