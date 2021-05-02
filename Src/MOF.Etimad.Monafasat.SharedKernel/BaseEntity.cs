using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.SharedKernal
{
    public class BaseEntity : IDataErrorInfo, IStateObject
    {

        #region IDataErrorInfo Members

        /// <summary>
        /// Determines if current business object has no validation issue or error.
        /// </summary>
        [NotMapped]
        public bool IsValid { get; protected set; }

        /// <summary>
        /// Represents all the validation result of current business object.
        /// This property is influenced by Validate method call.
        /// </summary>
        [NotMapped]
        public ObservableCollection<ValidationResult> ValidationResults { get; protected set; }

        /// <summary>
        /// Validates all properties of current business object.
        /// This method influence IsValid, and ValidationResults properties.
        /// </summary>
        public virtual bool Validate()
        {
            ValidationResults = new ObservableCollection<ValidationResult>();

            return IsValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(this, new ValidationContext(this, null, null), ValidationResults, true);
        }

        /// <summary>
        /// Gets an error message indicating what is wrong with this object.
        /// Invoking this properties will influence IsValid, and ValidationResults properties.
        /// </summary>
        [NotMapped]
        public string Error
        {
            get
            {
                Validate();

                var result = from x in ValidationResults
                             from y in x.ErrorMessage
                             select y;

                return string.Join(Environment.NewLine, result);
            }
        }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// Invoking this indexer will influence IsValid, and ValidationResults properties.
        /// </summary>
        /// <param name="propertyName">The name of the property whose error message to get.</param>
        /// <returns>The error message for the property. The default is an empty string ("").</returns>
        public string this[string propertyName]
        {
            get
            {
                Validate();

                ValidationResult result = (from x in ValidationResults
                                           from y in x.MemberNames
                                           where y == propertyName
                                           select x).FirstOrDefault();

                return (result != null) ? result.ErrorMessage : string.Empty;
            }
        }

        #endregion

        /// Determin the current state of the Entity
        /// </summary>
        [NotMapped]
        public ObjectState State { get; protected set; }

        /// <summary>
        /// Change the Entity State to Modified if its not added or deleted
        /// </summary>
        protected virtual void SetModifiedIfNot()
        {
            if (State == ObjectState.Unchanged)
            {
                State = ObjectState.Modified;
            }
        }

        protected virtual void EntityCreated()
        {
            this.State = ObjectState.Added;
        }

        protected virtual void EntityUpdated()
        {
            SetModifiedIfNot();
        }

        protected virtual void EntityDeleted()
        {
            this.State = ObjectState.Deleted;
        }
        public virtual void Unchanged()
        {

            State = ObjectState.Unchanged;

        }
    }
}
