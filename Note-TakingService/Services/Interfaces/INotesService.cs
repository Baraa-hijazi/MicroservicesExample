using System.Threading.Tasks;
using NoteTakingService.Core.Dtos;

namespace NoteTakingService.Services.Interfaces
{
    public interface INotesService
    {
        Task<PagedResultDto<NotesDto>> GetNotes(int? pageIndex, int? pageSize);
        Task<NotesDto> GetNote(int id);
        Task<NotesDto> CreateNote(NotesDto notesDto);
        Task<NotesDto> EditNotes(int id, EditNoteDto editNoteDto);
        Task<string> DeleteNote(int id);
    }
}