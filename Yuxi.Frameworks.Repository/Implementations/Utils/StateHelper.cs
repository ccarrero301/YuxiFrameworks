﻿namespace Yuxi.Frameworks.Repository.Implementations.Utils
{
    using System;
    using System.Data.Entity;
    using TrackableEntities;

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

        public static TrackingState ConvertState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return TrackingState.Unchanged;

                case EntityState.Unchanged:
                    return TrackingState.Unchanged;

                case EntityState.Added:
                    return TrackingState.Added;

                case EntityState.Deleted:
                    return TrackingState.Deleted;

                case EntityState.Modified:
                    return TrackingState.Modified;

                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }

        #endregion
    }
}