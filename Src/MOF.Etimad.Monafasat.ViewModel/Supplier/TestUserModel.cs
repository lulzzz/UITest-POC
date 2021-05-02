using System.Collections.Generic;
using System.Linq;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TestUserModel
    {        //
        // Summary:
        //     Gets or sets the subject identifier.
        public string SubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets the username.
        public string Username { get; set; }
        //
        // Summary:
        //     Gets or sets the password.
        public string Password { get; set; }
        //
        // Summary:
        //     Gets or sets the provider name.
        public string ProviderName { get; set; }
        //
        // Summary:
        //     Gets or sets the provider subject identifier.
        public string ProviderSubjectId { get; set; }
        //
        // Summary:
        //     Gets or sets if the user is active.
        public bool IsActive { get; set; }
        //
        // Summary:
        //     Gets or sets the claims.
        public ICollection<UserClaim> Claims { get; set; }

        public string UserCR()
        {
            var result = this.Claims.FirstOrDefault(x => x.type == "selectedCR");
            // Test for null to avoid issues during local testing
            return (result != null) ? result.value : string.Empty;
        }

    }
}
