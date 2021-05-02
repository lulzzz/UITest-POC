using Microsoft.EntityFrameworkCore;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class AnnouncementListJobQueries : IAnnouncementListJobQueries
    {
        private readonly IAppDbContext _context;
        public AnnouncementListJobQueries(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<AnnouncementSupplierTemplate>> getAllEndedTemplates()
        {
            return await _context.AnnouncementSupplierTemplate.Where(a => a.StatusId == (int)Enums.AnnouncementStatus.Approved && a.StatusId != (int)Enums.AnnouncementStatus.Ended && a.IsEffectiveList == true && a.EffectiveListDate < DateTime.Now).ToListAsync();
        }
    }
}
