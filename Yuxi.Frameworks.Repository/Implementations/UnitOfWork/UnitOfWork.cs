namespace Yuxi.Frameworks.Repository.Implementations.UnitOfWork
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Data;
    using System.Data.Common;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using Contracts.DataContext;
    using Contracts.Repository;
    using Contracts.UnitOfWork;
    using Repository;
    using CommonServiceLocator;
    using TrackableEntities;

    public class UnitOfWork : IUnitOfWorkAsync
    {
        #region Members

        private IDataContextAsync _dataContext;

        private bool _disposed;

        private ObjectContext _objectContext;

        private DbTransaction _transaction;

        private Dictionary<string, dynamic> _commandRepositories;

        #endregion

        #region Constructors

        public UnitOfWork(IDataContextAsync dataContext)
        {
            _dataContext = dataContext;
            _commandRepositories = new Dictionary<string, dynamic>();
        }

        #endregion

        #region Clean Up

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                try
                {
                    if (_objectContext != null)
                    {
                        if (_objectContext.Connection.State == ConnectionState.Open)
                            _objectContext.Connection.Close();

                        _objectContext.Dispose();
                        _objectContext = null;
                    }
                    if (_dataContext != null)
                    {
                        _dataContext.Dispose();
                        _dataContext = null;
                    }
                }
                catch (ObjectDisposedException)
                {
                    // do nothing, the objectContext has already been disposed
                }
                _commandRepositories = null;
            }

            _disposed = true;
        }

        #endregion

        #region Public Methods

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class, ITrackable
        {
            return ServiceLocator.IsLocationProviderSet
                ? ServiceLocator.Current.GetInstance<IRepository<TEntity>>()
                : RepositoryAsync<TEntity>();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dataContext.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dataContext.SaveChangesAsync(cancellationToken);
        }

        public IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, ITrackable
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                return ServiceLocator.Current.GetInstance<IRepositoryAsync<TEntity>>();
            }

            if (_commandRepositories == null)
            {
                _commandRepositories = new Dictionary<string, dynamic>();
            }

            var type = typeof(TEntity).Name;

            if (_commandRepositories.ContainsKey(type))
            {
                return (IRepositoryAsync<TEntity>) _commandRepositories[type];
            }

            var repositoryType = typeof(Repository<>);

            _commandRepositories.Add(type,
                Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

            return _commandRepositories[type];
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified)
        {
            _objectContext = ((IObjectContextAdapter) _dataContext).ObjectContext;
            if (_objectContext.Connection.State != ConnectionState.Open)
            {
                _objectContext.Connection.Open();
            }

            _transaction = _objectContext.Connection.BeginTransaction(isolationLevel);
        }

        public bool Commit()
        {
            _transaction.Commit();
            return true;
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _dataContext.SyncObjectsStatePostCommit();
        }

        #endregion
    }
}