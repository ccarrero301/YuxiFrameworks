namespace Yuxi.Frameworks.Repository.Standard.Implementations.Query
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Linq.Expressions;
    using Contracts.Query;
    using Repository;
    using TrackableEntities.Common.Core;
    using Specification.Standard.Base;

    public sealed class QueryFluent<TEntity> : IQueryFluent<TEntity> where TEntity : class, ITrackable
    {
        #region Read Only Members

        private readonly ExpressionSpecification<TEntity> _specification;

        private readonly List<Expression<Func<TEntity, object>>> _includes;

        private readonly Repository<TEntity> _repository;

        #endregion

        #region Members

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;

        #endregion

        #region Constructors

        public QueryFluent(Repository<TEntity> repository)
        {
            _repository = repository;

            _includes = new List<Expression<Func<TEntity, object>>>();
        }

        public QueryFluent(Repository<TEntity> repository, ExpressionSpecification<TEntity> specification)
            : this(repository)
        {
            _specification = specification;
        }

        #endregion

        #region Public Methods

        public IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includes.Add(expression);
            return this;
        }

        public IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount)
        {
            totalCount = _repository.Select(_specification.ToExpression()).Count();

            return _repository.Select(_specification.ToExpression(), _orderBy, _includes, page, pageSize);
        }

        public IEnumerable<TEntity> Select()
        {
            return _repository.Select(_specification.ToExpression(), _orderBy, _includes);
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _repository.Select(_specification.ToExpression(), _orderBy, _includes).Select(selector);
        }

        public async Task<IEnumerable<TEntity>> SelectAsync()
        {
            return await _repository.SelectAsync(_specification.ToExpression(), _orderBy, _includes);
        }

        #endregion
    }
}