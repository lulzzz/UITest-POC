using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel.Tender
{
    public class ActivityVersionModel
    {

        public int ActivityVersionId { get; set; }
        public string ActivityIdsString { get; set; }

        public List<int> ActivityIds
        {
            get
            {
                if (ActivityIdsString != null)
                {
                    return new List<int>(Array.ConvertAll(ActivityIdsString.Split(','), int.Parse));
                }
                else
                {
                    return null;
                }
            }
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
            object value = property.GetValue(this);
            dictionary.Add(property.Name, value);
        }
    }
}
