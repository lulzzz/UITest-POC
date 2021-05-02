using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Data;
using MOF.Etimad.SharedKernel;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class EnquiryCommands : IEnquiryCommands
    {


        private readonly IAppDbContext _context;
        public EnquiryCommands(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<Enquiry> CreateAsync(Enquiry enquiry)
        {
            await _context.Enquiries.AddAsync(enquiry);
            await _context.SaveChangesAsync();
            return enquiry;
        }


        public async Task<Enquiry> UpdateAsync(Enquiry enquiry)
        {
            Check.ArgumentNotNull(nameof(enquiry), enquiry);
            _context.Enquiries.Update(enquiry);
            await _context.SaveChangesAsync();
            return enquiry;
        }

        public async Task<EnquiryReply> CreateReplyAsync(EnquiryReply enquiryReply)

        {
            await _context.EnquiryReplies.AddAsync(enquiryReply);
            await _context.SaveChangesAsync();
            return enquiryReply;
        }

        public async Task<EnquiryReply> UpdateReplyAsync(EnquiryReply enquiryReply)
        {
            Check.ArgumentNotNull(nameof(enquiryReply), enquiryReply);
            _context.EnquiryReplies.Update(enquiryReply);
            await _context.SaveChangesAsync();
            return enquiryReply;
        }

    }
}
