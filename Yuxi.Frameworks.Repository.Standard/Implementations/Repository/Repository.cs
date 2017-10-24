namespace Yuxi.Frameworks.Repository.Standard.Implementations.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using Contracts.DataContext;
    using Contracts.Query;
    using Contracts.Repository;
    using Contracts.UnitOfWork;
    using DataContext;
    using Query;
    using Utils;
    using EFCore = Microsoft.EntityFrameworkCore;
    using TrackableEntities.Common.Core;
    using Specification.Standard.Base;

    public class Repository<TAgreggate> : IRepositoryAsync<TAgreggate> where TAgreggate : class, ITrackable
    {
        #region Read Only Members

        private readonly IDataContextAsync _context;

        private readonly EFCore.DbSet<TAgreggate> _dbSet;

        private readonly IUnitOfWorkAsync _unitOfWork;

        #endregion

        #region Constructors

        public Repository(IDataContextAsync context, IUnitOfWorkAsync unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;

            switch (context)
            {
                case EFCore.DbContext dbContext:
                    _dbSet = dbContext.Set<TAgreggate>();
                    break;
                case FakeDbContext fakeContext:
                    _dbSet = fakeContext.Set<TAgreggate>();
                    break;
            }
        }

        #endregion

        #region Public Methods

        public IRepository<T> GetRepository<T>() where T : class, ITrackable
        {
            return _unitOfWork.Repository<T>();
        }

        #region Commands

        public virtual void Add(TAgreggate entity)
        {
            entity.TrackingState = TrackingState.Added;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void Update(TAgreggate entity)
        {
            entity.TrackingState = TrackingState.Modified;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void Delete(TAgreggate entity)
        {
            entity.TrackingState = TrackingState.Deleted;
            _dbSet.Attach(entity);
            _context.SyncObjectState(entity);
        }

        public virtual void Delete(params object[] keyValues)
        {
            var entity = _dbSet.Find(keyValues);

            Delete(entity);
        }

        public virtual async Task<bool> DeleteAsync(params object[] keyValues)
        {
            return await DeleteAsync(CancellationToken.None, keyValues);
        }

        public virtual async Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            var entity = await FindAsync(cancellationToken, keyValues);

            if (entity == null)
            {
                return false;
            }

            entity.TrackingState = TrackingState.Deleted;
            _dbSet.Attach(entity);

            return true;
        }

        public virtual TAgreggate Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }

        public virtual async Task<TAgreggate> FindAsync(params object[] keyValues)
        {
            return await _dbSet.FindAsync(keyValues);
        }

        public virtual async Task<TAgreggate> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return await _dbSet.FindAsync(cancellationToken, keyValues);
        }

        public void UpsertGraph(TAgreggate entity)
        {
            _context.SyncObjectState(entity);
        }

        #endregion

        #region Querys

        public IQueryFluent<TAgreggate> Query()
        {
            return new QueryFluent<TAgreggate>(this, new AllExpressionSpecification<TAgreggate>());
        }

        public virtual IQueryFluent<TAgreggate> Query(ExpressionSpecification<TAgreggate> specification)
        {
            return new QueryFluent<TAgreggate>(this, specification);
        }

        public IQueryable<TAgreggate> Queryable()
        {
            return _dbSet;
        }

        #endregion

        #endregion

        #region Internal Methods

        internal IQueryable<TAgreggate> Select(
            Expression<Func<TAgreggate, bool>> filter = null,
            Func<IQueryable<TAgreggate>, IOrderedQueryable<TAgreggate>> orderBy = null,
            List<Expression<Func<TAgreggate, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<TAgreggate> query = _dbSet;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }

        internal async Task<IEnumerable<TAgreggate>> SelectAsync(
            Expression<Func<TAgreggate, bool>> filter = null,
            Func<IQueryable<TAgreggate>, IOrderedQueryable<TAgreggate>> orderBy = null,
            List<Expression<Func<TAgreggate, object>>> includes = null,
            int? page = null,
            int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }

        #endregion
    }
}