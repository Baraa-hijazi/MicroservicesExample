using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NoteTakingService.Core.Dtos;
using NoteTakingService.Core.Entities;
using NoteTakingService.Persistence.Interfaces;
using NoteTakingService.Services.Interfaces;

namespace NoteTakingService.Services
{
    public class NotesService : INotesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NotesService(IMapper mapper, IUnitOfWork unitOfWork) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResultDto<NotesDto>> GetNotes(int? pageIndex, int? pageSize)
        {
            var notes = await _unitOfWork.NotesRepository.GetAllIncludedPagnation(f => f.Content != null, pageIndex, pageSize);
            return _mapper.Map<PagedResultDto<Notes>, PagedResultDto<NotesDto>>(notes);
        }

        public async Task<NotesDto> GetNote(int id)
        {
            var notes = await _unitOfWork.NotesRepository.GetById(id);
            if (notes == null)
                throw new Exception("Not Found... ");
            return _mapper.Map<Notes, NotesDto>(notes);
        }

        public async Task<NotesDto> CreateNote([FromForm] NotesDto notesDto)
        {
            var note = _mapper.Map<NotesDto, Notes>(notesDto);
            _unitOfWork.NotesRepository.Add(note);
            await _unitOfWork.CompleteAsync();
            var result = _mapper.Map<Notes, NotesDto>(note);
            return result;
        }

        public async Task<NotesDto> EditNotes(int id, EditNoteDto editNoteDto)
        {
            var notes = await _unitOfWork.NotesRepository.GetById(id);
            if (notes == null)
                throw new Exception("Not Found... ");
            var note = _mapper.Map(editNoteDto, notes);
            
            _unitOfWork.NotesRepository.Update(note);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Notes, NotesDto>(note);
        }

        public async Task<string> DeleteNote(int id)
        {
            var notes = await _unitOfWork.NotesRepository.GetById(id);
            if (notes == null)
                throw new Exception("Not Found... ");

            _unitOfWork.NotesRepository.Delete(notes);
            await _unitOfWork.CompleteAsync();

            return ("Note was deleted... ");
        }
    }
}