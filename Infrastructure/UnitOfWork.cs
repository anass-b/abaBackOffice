// -------------------- UnitOfWork --------------------
using abaBackOffice.DataAccessLayer;
using abaBackOffice.Interfaces;
using abaBackOffice.Interfaces.Repositories;
using abaBackOffice.Models;
using abaBackOffice.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace abaBackOffice.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AbaDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        private readonly ILogger<UserRepository> _userLogger;
        private readonly ILogger<DocumentRepository> _documentLogger;
        private readonly ILogger<SubscriptionRepository> _subscriptionLogger;
        private readonly ILogger<VideoRepository> _videoLogger;
        private readonly ILogger<AbllsTaskRepository> _abllsTaskLogger;
        private readonly ILogger<BlogPostRepository> _blogPostLogger;
        private readonly ILogger<BlogCommentRepository> _blogCommentLogger;
        private readonly ILogger<OtpCodeRepository> _otpCodeLogger;
        private readonly ILogger<ReinforcementProgramRepository> _reinforcementProgramLogger;
        private readonly ILogger<ReinforcerAgentRepository> _reinforcerAgentLogger;
        private readonly ILogger<CategoryRepository> _categoryLogger;
        private readonly ILogger<EvaluationCriteriaRepository> _evaluationCriteriaLogger;
        private readonly ILogger<MaterialPhotoRepository> _materialPhotoLogger;
        private readonly ILogger<BaselineContentRepository> _baselineContentLogger;
        private readonly ILogger<EvaluationCriteriaMaterialRepository> _evaluationCriteriaMaterialLogger;
        private readonly ILogger<DomainRepository> _domainLogger;

        private IUserRepository _userRepository;
        private IDocumentRepository _documentRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private IVideoRepository _videoRepository;
        private IAbllsTaskRepository _abllsTaskRepository;
        private IBlogPostRepository _blogPostRepository;
        private IBlogCommentRepository _blogCommentRepository;
        private IOtpCodeRepository _otpCodeRepository;
        private IReinforcementProgramRepository _reinforcementProgramRepository;
        private IReinforcerAgentRepository _reinforcerAgentRepository;
        private ICategoryRepository _categoryRepository;
        private IEvaluationCriteriaRepository _evaluationCriteriaRepository;
        private IMaterialPhotoRepository _materialPhotoRepository;
        private IBaselineContentRepository _baselineContentRepository;
        private IEvaluationCriteriaMaterialRepository _evaluationCriteriaMaterialRepository;
        private IDomainRepository _domainRepository;

        private IDbContextTransaction _transaction;

        public UnitOfWork(
            AbaDbContext context,
            ILogger<UnitOfWork> logger,
            ILogger<UserRepository> userLogger,
            ILogger<DocumentRepository> documentLogger,
            ILogger<SubscriptionRepository> subscriptionLogger,
            ILogger<VideoRepository> videoLogger,
            ILogger<AbllsTaskRepository> abllsTaskLogger,
            ILogger<BlogPostRepository> blogPostLogger,
            ILogger<BlogCommentRepository> blogCommentLogger,
            ILogger<OtpCodeRepository> otpCodeLogger,
            ILogger<ReinforcementProgramRepository> reinforcementProgramLogger,
            ILogger<ReinforcerAgentRepository> reinforcerAgentLogger,
            ILogger<CategoryRepository> categoryLogger,
            ILogger<EvaluationCriteriaRepository> evaluationCriteriaLogger,
            ILogger<MaterialPhotoRepository> materialPhotoLogger,
            ILogger<BaselineContentRepository> baselineContentLogger,
            ILogger<EvaluationCriteriaMaterialRepository> evaluationCriteriaMaterialLogger,
            ILogger<DomainRepository> domainLogger
        )
        {
            _context = context;
            _logger = logger;
            _userLogger = userLogger;
            _documentLogger = documentLogger;
            _subscriptionLogger = subscriptionLogger;
            _videoLogger = videoLogger;
            _abllsTaskLogger = abllsTaskLogger;
            _blogPostLogger = blogPostLogger;
            _blogCommentLogger = blogCommentLogger;
            _otpCodeLogger = otpCodeLogger;
            _reinforcementProgramLogger = reinforcementProgramLogger;
            _categoryLogger = categoryLogger;
            _evaluationCriteriaLogger = evaluationCriteriaLogger;
            _materialPhotoLogger = materialPhotoLogger;
            _baselineContentLogger = baselineContentLogger;
            _evaluationCriteriaMaterialLogger = evaluationCriteriaMaterialLogger;
            _domainLogger = domainLogger;
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context, _userLogger);
        public IDocumentRepository DocumentRepository => _documentRepository ??= new DocumentRepository(_context, _documentLogger);
        public ISubscriptionRepository SubscriptionRepository => _subscriptionRepository ??= new SubscriptionRepository(_context, _subscriptionLogger);
        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context, _videoLogger);
        public IAbllsTaskRepository AbllsTaskRepository => _abllsTaskRepository ??= new AbllsTaskRepository(_context, _abllsTaskLogger);
        public IBlogPostRepository BlogPostRepository => _blogPostRepository ??= new BlogPostRepository(_context, _blogPostLogger);
        public IBlogCommentRepository BlogCommentRepository => _blogCommentRepository ??= new BlogCommentRepository(_context, _blogCommentLogger);
        public IOtpCodeRepository OtpCodeRepository => _otpCodeRepository ??= new OtpCodeRepository(_context, _otpCodeLogger);
        public IReinforcementProgramRepository ReinforcementProgramRepository => _reinforcementProgramRepository ??= new ReinforcementProgramRepository(_context, _reinforcementProgramLogger);
        public IReinforcerAgentRepository ReinforcerAgentRepository => _reinforcerAgentRepository ??= new ReinforcerAgentRepository(_context, _reinforcerAgentLogger);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context, _categoryLogger);
        public IEvaluationCriteriaRepository EvaluationCriteriaRepository => _evaluationCriteriaRepository ??= new EvaluationCriteriaRepository(_context, _evaluationCriteriaLogger);
        public IMaterialPhotoRepository MaterialPhotoRepository => _materialPhotoRepository ??= new MaterialPhotoRepository(_context, _materialPhotoLogger);
        public IBaselineContentRepository BaselineContentRepository => _baselineContentRepository ??= new BaselineContentRepository(_context, _baselineContentLogger);
        public IEvaluationCriteriaMaterialRepository EvaluationCriteriaMaterialRepository => _evaluationCriteriaMaterialRepository ??= new EvaluationCriteriaMaterialRepository(_context, _evaluationCriteriaMaterialLogger);
        public IDomainRepository DomainRepository => _domainRepository ??= new DomainRepository(_context, _domainLogger);
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
            }
        }
        public async Task RollbackAsync()
        {
            // Annule toutes les modifications non sauvegardées
            foreach (var entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        await entry.ReloadAsync();
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public void DetachEntity<T>(T entity) where T : class
        {
            var entry = _context.Entry(entity);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
