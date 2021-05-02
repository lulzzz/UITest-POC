using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MOF.Etimad.Monafasat.SharedKernal
{
    /// <summary>
    /// Represent the base criteria parameters that are used in Searching methods.
    /// </summary>
    [Serializable]
    public class SearchCriteria
    {
        /// <summary>
        /// The size of page that is intended to be retrieved.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// The number of page to be retrieved.
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// The property name to sort with
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// The sort direction (ASC, DESC)
        /// </summary>
        public string SortDirection { get; set; }

        public SearchCriteria()
        {
            PageSize = 12;
            PageNumber = 1;
            SortDirection = MOF.Etimad.Monafasat.SharedKernal.SortDirection.Descending;
        }

        public Dictionary<string, object> ToDictionary()
        {
            try
            {
                var dictionary = new Dictionary<string, object>();
                foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(this))
                    AddPropertyToDictionary(property, dictionary);
                return dictionary;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        private void AddPropertyToDictionary(PropertyDescriptor property, Dictionary<string, object> dictionary)
        {
            //bool exists = dict.TryGetValue(Key, out sValue);
            object value = property.GetValue(this);
            dictionary.Add(property.Name, value);
        }
    }
}
