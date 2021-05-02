using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class Committee
    {
        public void Update(string name, string address, string phone, string fax, string email, string postalCode,
            string zipCode)
        {
            CommitteeName = name;
            Address = address;
            Phone = phone;
            Fax = fax;
            Email = email;
            PostalCode = postalCode;
            ZipCode = zipCode;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void Test_AddCommitteeUsers(List<CommitteeUser> committeeUsers)
        {
            this.CommitteeUsers = committeeUsers;
        }

        public void Test_AddCommitteeTypeId(int committeeType)
        {
            this.CommitteeTypeId = committeeType;
        }

    }
}
