using MOF.Etimad.Monafasat.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
   public interface IIDMCommands
    {
        Task<GovAgency> CreateGovAgencyAsync(GovAgency govAgency);
        GovAgency UpdateGovAgencyAsync(GovAgency govAgency);
        Task<User> CreateUserAsync(User User);
        User UpdateUserAsync(User User);
        Task<UserAgencyRole> CreateUserAgencyRoleAsync(UserAgencyRole userAgencyRole);
        void DeleteUserAgencyRoleAsync(List<UserAgencyRole> userAgencyRoles);
        Task SaveAsync();
    }
}
