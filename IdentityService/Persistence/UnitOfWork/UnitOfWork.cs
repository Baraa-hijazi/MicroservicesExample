using System.Threading.Tasks;
using IdentityService.Core.Entities;
using IdentityService.Persistence.Contexts;
using IdentityService.Persistence.Interfaces;
using IdentityService.Persistence.Repositories;

 namespace IdentityService.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    { 
        public  readonly ApplicationDbContext _context;
            public IBaseRepository<ApplicationUser> AccountRepository { get; }
        public UnitOfWork(ApplicationDbContext context)
        { 
            AccountRepository = new BaseRepository<ApplicationUser>(context);
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
