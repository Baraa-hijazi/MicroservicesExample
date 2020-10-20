using AutoMapper;
using NoteTakingService.Core.Dtos;
using NoteTakingService.Core.Entities;

namespace NoteTakingService.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Notes, NotesDto>().ReverseMap();
            CreateMap<EditNoteDto, Notes>().ReverseMap();
            CreateMap(typeof(PagedResultDto<>), typeof(PagedResultDto<>));
        }
    }
}