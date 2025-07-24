// -------------------- IUnitOfWork.cs --------------------
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Repositories;

namespace abaBackOffice.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IDocumentRepository DocumentRepository { get; }
        ISubscriptionRepository SubscriptionRepository { get; }
        IVideoRepository VideoRepository { get; }
        IAbllsTaskRepository AbllsTaskRepository { get; }
        IAbllsVideoRepository AbllsVideoRepository { get; }
        IBlogPostRepository BlogPostRepository { get; }
        IBlogCommentRepository BlogCommentRepository { get; }
        IReinforcementProgramRepository ReinforcementProgramRepository { get; }
        IReinforcerAgentRepository ReinforcerAgentRepository { get; }
        IOtpCodeRepository OtpCodeRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task CommitAsync();
        Task RollbackAsync();
        void DetachEntity<T>(T entity) where T : class;
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
