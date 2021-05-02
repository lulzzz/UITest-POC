using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class VerificationQueries : IVerificationQueries
    {
        private readonly IAppDbContext _context;
        public VerificationQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<VerificationCode> FindVerificationCode(int codeTypeId, int userId, int TypeId)
        {
            var entity = await _context.VerificationCode.Where(x => x.CodeTypeId == codeTypeId && x.VerificationTypeId == TypeId && x.UserId == userId).OrderByDescending(a => a.VerificationCodeId).FirstOrDefaultAsync();
            return entity;
        }
    }
}
