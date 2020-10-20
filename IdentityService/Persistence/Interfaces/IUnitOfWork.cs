using System.Threading.Tasks;
using IdentityService.Core.Entities;

 namespace IdentityService.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<ApplicationUser> AccountRepository { get; }
        Task CompleteAsync();
    }
}
