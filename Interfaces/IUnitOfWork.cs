using Consultation.Interfaces;
using Consultation.Models;
using System.Threading.Tasks;

namespace Consultation.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Message> MessageRepository { get; }
        IBaseRepository<Product> ProductRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}