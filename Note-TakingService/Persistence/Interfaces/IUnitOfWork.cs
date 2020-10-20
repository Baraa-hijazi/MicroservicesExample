﻿using NoteTakingService.Core.Entities;
using System.Threading.Tasks;
 using NoteTakingService.Persistence.Interfaces;

 namespace NoteTakingService.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Notes> NotesRepository { get; }

        Task CompleteAsync();
    }
}
