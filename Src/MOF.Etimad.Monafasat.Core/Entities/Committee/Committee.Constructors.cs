
namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class Committee
    {
        public Committee() { }
        public Committee(int committeeId, string agencyCode, string committeeName, string address, string phone, string fax,
            string email, string postalCode, string zipCode, bool? isActive = true)
        {
            CommitteeId = committeeId;
            AgencyCode = agencyCode;
            CommitteeName = committeeName;
            Address = address;
            Phone = phone;
            Fax = fax;
            Email = email;
            PostalCode = postalCode;
            ZipCode = zipCode;
            EntityCreated();
        }
    }
}
