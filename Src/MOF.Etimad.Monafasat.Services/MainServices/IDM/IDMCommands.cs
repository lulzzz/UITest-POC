using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class IDMCommands : IIDMCommands
    {

        private IAppDbContext _context;

        public IDMCommands(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<GovAgency> CreateGovAgencyAsync(GovAgency govAgency)
        {
            await _context.GovAgencies.AddAsync(govAgency);
            //await _context.SaveChangesAsync();
            return govAgency;
        }

        public GovAgency UpdateGovAgencyAsync(GovAgency govAgency)
        {
            _context.GovAgencies.Update(govAgency);
            //await _context.SaveChangesAsync();
            return govAgency;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            //await _context.SaveChangesAsync();
            return user;
        }

        public User UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            //await _context.SaveChangesAsync();
            return user;
        }
        public async Task<UserAgencyRole> CreateUserAgencyRoleAsync(UserAgencyRole userAgencyRole)
        {
            await _context.UserAgencyRoles.AddAsync(userAgencyRole);
            //await _context.SaveChangesAsync();
            return userAgencyRole;
        }

        public void DeleteUserAgencyRoleAsync(List<UserAgencyRole> userAgencyRoles)
        {
            _context.UserAgencyRoles.RemoveRange(userAgencyRoles);
            //await _context.SaveChangesAsync();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
