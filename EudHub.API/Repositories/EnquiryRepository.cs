using EduHub.API.Models;
using EudHub.API.Models;
using EudHub.API.Services;
using Microsoft.EntityFrameworkCore;

namespace EudHub.API.Repositories
{
    public class EnquiryRepository : IEnquriyService
    {
        private readonly EduHubInfoContext _enquiryContext;

        public EnquiryRepository(EduHubInfoContext enquiryContext)
        {
            _enquiryContext = enquiryContext;
        }

        public async Task<Enquiry> AddEnquiryAsync(Enquiry saveEnquiry)
        {
            await _enquiryContext.Enquiries.AddAsync(saveEnquiry);
            await _enquiryContext.SaveChangesAsync();
            return saveEnquiry;
        }

        public async Task<IEnumerable<Enquiry>> GetEnquiriesAsync()
        {
          return await _enquiryContext.Enquiries.ToListAsync();
        }

        public int GetusermaxId()
        {
            return _enquiryContext.Enquiries.Max(u => (int?)u.enquiryId) ?? 0;
        }
    }
}
