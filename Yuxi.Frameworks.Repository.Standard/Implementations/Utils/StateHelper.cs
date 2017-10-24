namespace Yuxi.Frameworks.Repository.Standard.Implementations.Utils
{
    using System;
    using System.Data.Entity;
    using EFCore = Microsoft.EntityFrameworkCore;
    using TrackableEntities.Common.Core;

    public class StateHelper
    {
        #region Public Methods

        public static EntityState ConvertState(TrackingState state)
        {
            switch (state)
            {
                case TrackingState.Added:
                    return EntityState.Added;

                case TrackingState.Modified:
                    return EntityState.Modified;

                case TrackingState.Deleted:
                    return EntityState.Deleted;

                default:
                    return EntityState.Unchanged;
            }
        }

        public static TrackingState ConvertState(EFCore.EntityState state)
        {
            switch (state)
            {
                case EFCore.EntityState.Detached:
                    return TrackingState.Unchanged;

                case EFCore.EntityState.Unchanged:
                    return TrackingState.Unchanged;

                case EFCore.EntityState.Added:
                    return TrackingState.Added;

                case EFCore.EntityState.Deleted:
                    return TrackingState.Deleted;

                case EFCore.EntityState.Modified:
                    return TrackingState.Modified;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }

            #endregion
        }
    }
}