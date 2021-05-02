using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.SharedKernel
{
    public delegate void notifyMe(object sender, AuditableEntity entity);

    public abstract class AuditableEntity : BaseEntity
    {
        /// <summary>
        /// System creation date time of this entity, set by the system.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The user who create the entity, set by the system.
        /// </summary>
        [StringLength(256)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// System update date time of this entity, set by the system.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// The user who update the entity, set by the system.
        /// </summary>
        [StringLength(256)]
        public string UpdatedBy { get; set; }

        public bool? IsActive { get; protected set; }

        protected override void EntityCreated()
        {
            this.CreatedAt = DateTime.Now;
            this.IsActive = true;
            this.State = ObjectState.Added;
        }

        protected override void EntityUpdated()
        {
            base.EntityUpdated();
        }

    }
}
