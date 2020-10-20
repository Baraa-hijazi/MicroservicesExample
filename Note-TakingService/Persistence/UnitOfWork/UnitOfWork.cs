﻿using NoteTakingService.Core.Entities;
using NoteTakingService.Persistence.Repositories;
using System.Threading.Tasks;
 using NoteTakingService.Persistence.Interfaces;
 using NoteTakingService.Persistence.Contexts;

 namespace NoteTakingService.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    { 
        public  readonly ApplicationDbContext _context;
        public IBaseRepository<Notes> NotesRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            NotesRepository = new BaseRepository<Notes>(context);
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
