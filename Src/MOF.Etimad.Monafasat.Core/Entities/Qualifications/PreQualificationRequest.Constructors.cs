
namespace MOF.Etimad.Monafasat.Core.Entities
{
  public partial  class PreQualificationRequest
    {
         public PreQualificationRequest() { }
        public PreQualificationRequest(int committeeId, int agencyId, string committeeName, string address, string phone, string fax, string email, string postalCode, string zipCode, bool? isActive = true)
        {
            //CommitteeId = committeeId;
            //AgencyId = agencyId;
            //CommitteeName = committeeName;
            //Address = address;
            //Phone = phone;
            //Fax = fax;
            //Email = email;
            //PostalCode = postalCode;
            //ZipCode = zipCode;
            EntityCreated();
        }
     }
}
