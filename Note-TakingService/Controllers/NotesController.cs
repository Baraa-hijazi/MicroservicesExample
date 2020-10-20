using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoteTakingService.Core.Dtos;
using NoteTakingService.Services.Interfaces;

namespace NoteTakingService.Controllers
{
    [Route("/[Controller]")]
    [ApiController]
    public class NotesController : Controller
    {
        private readonly INotesService _notesService;

        public NotesController(INotesService notesService)
        {
            _notesService = notesService;
        }
        
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFolders([FromBody] NotesDto notesDto) => Ok(await _notesService.CreateNote(notesDto));

        [HttpGet("Get-By-Id")]
        public async Task<IActionResult> GetFolders(int id) => Ok(await _notesService.GetNote(id));

        [HttpGet("Get-All-Paged")]
        public async Task<IActionResult> GetPagedFolders([FromQuery] int? pageIndex, [FromQuery] int? pageSize) => Ok(await _notesService.GetNotes(pageIndex, pageSize));
    
        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateFolders(int id, [FromForm] EditNoteDto editNoteDto) => Ok(await _notesService.EditNotes(id, editNoteDto));

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteFolders(int id) => Ok(await _notesService.DeleteNote(id));
    }
}
