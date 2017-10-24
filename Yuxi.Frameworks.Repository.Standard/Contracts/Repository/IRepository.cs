namespace Yuxi.Frameworks.Repository.Standard.Contracts.Repository
{
    using System.Linq;
    using Query;
    using Specification.Standard.Base;
    using TrackableEntities.Common.Core;
    
    public interface IRepository<TAgreggate> where TAgreggate : class, ITrackable
    {
        #region Public Methods

        IRepository<T> GetRepository<T>() where T : class, ITrackable;

        #region Commands

        void Add(TAgreggate entity);

        void Update(TAgreggate entity);

        void Delete(TAgreggate entity);

        void Delete(params object[] keyValues);

        TAgreggate Find(params object[] keyValues);

        void UpsertGraph(TAgreggate entity);

        #endregion

        #region Querys

        IQueryFluent<TAgreggate> Query(ExpressionSpecification<TAgreggate> specification);

        IQueryFluent<TAgreggate> Query();

        IQueryable<TAgreggate> Queryable();

        #endregion

        #endregion
    }
}