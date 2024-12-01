using AutoMapper;
using MyOwnSummary_API.Models;
using MyOwnSummary_API.Models.Dtos.CategoryDtos;
using MyOwnSummary_API.Models.Dtos.DictionaryDtos;
using MyOwnSummary_API.Models.Dtos.LanguageDtos;
using MyOwnSummary_API.Models.Dtos.NoteDtos;
using MyOwnSummary_API.Models.Dtos.UserDtos;

namespace MyOwnSummary_API.Profiles
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<CreateLanguageDto, Language>().ReverseMap();
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<CreateNoteDto, Note>().ReverseMap();
        }
    }
}
