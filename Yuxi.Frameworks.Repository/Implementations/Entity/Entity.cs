namespace Yuxi.Frameworks.Repository.Implementations.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using TrackableEntities;

    public abstract class Entity : ITrackable
    {
        #region Public Properties

        [NotMapped]
        public TrackingState TrackingState { get; set; }

        [NotMapped]
        public ICollection<string> ModifiedProperties { get; set; }

        #endregion
    }
}