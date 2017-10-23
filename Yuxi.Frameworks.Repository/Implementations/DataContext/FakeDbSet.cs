namespace Yuxi.Frameworks.Repository.Implementations.DataContext
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using TrackableEntities;

    public abstract class FakeDbSet<TEntity> : DbSet<TEntity>, IDbSet<TEntity> where TEntity : Entity.Entity, new()
    {
        #region Read Only Members

        private readonly ObservableCollection<TEntity> _items;
        private readonly IQueryable _query;

        #endregion

        #region Constructors

        protected FakeDbSet()
        {
            _items = new ObservableCollection<TEntity>();
            _query = _items.AsQueryable();
        }

        #endregion

        #region Explicit Implementations

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #endregion

        #region Public Properties

        public IEnumerator<TEntity> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        public Expression Expression => _query.Expression;

        public Type ElementType => _query.ElementType;

        public IQueryProvider Provider => _query.Provider;

        #endregion

        #region Public Overriden Methods

        public override TEntity Add(TEntity entity)
        {
            _items.Add(entity);
            return entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            _items.Remove(entity);
            return entity;
        }

        public override TEntity Attach(TEntity entity)
        {
            switch (entity.TrackingState)
            {
                case TrackingState.Modified:
                    _items.Remove(entity);
                    _items.Add(entity);
                    break;

                case TrackingState.Deleted:
                    _items.Remove(entity);
                    break;

                case TrackingState.Unchanged:
                case TrackingState.Added:
                    _items.Add(entity);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return entity;
        }

        public override TEntity Create()
        {
            return new TEntity();
        }

        public override TDerivedEntity Create<TDerivedEntity>()
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public override ObservableCollection<TEntity> Local => _items;

        #endregion
    }
}