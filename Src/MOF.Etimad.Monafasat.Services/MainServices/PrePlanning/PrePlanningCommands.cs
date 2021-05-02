using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class PrePlanningCommands : IPrePlanningCommands
    {
        private readonly IAppDbContext _context;
        public PrePlanningCommands(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<PrePlanning> CreateAsync(PrePlanning prePlanning)
        {
            await _context.PrePlannings.AddAsync(prePlanning);
            await _context.SaveChangesAsync();
            return prePlanning;
        }

        public async Task<PrePlanning> UpdateAsync(PrePlanning prePlanning)
        {
            _context.PrePlannings.Update(prePlanning);
            await _context.SaveChangesAsync();
            return prePlanning;
        }
    }
}
