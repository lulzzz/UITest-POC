using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
   public partial class PreQualificationRequest
    {
        public void Update(string name, string address, string phone, string fax, string email, string postalCode, string zipCode)
        {
           // CommitteeName = name;
            //Address = address;
            //Phone = phone;
            //Fax = fax;
            //Email = email;
            //PostalCode = postalCode;
            //ZipCode = zipCode;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
    }
}
