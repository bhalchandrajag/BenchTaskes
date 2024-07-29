using EudHub.API.Models;

namespace EudHub.API.Services
{
    public interface IEnquriyService
    {
        Task<IEnumerable<Enquiry>> GetEnquiriesAsync();
        Task<Enquiry> AddEnquiryAsync(Enquiry saveEnquiry);
        public int GetusermaxId();
    }
}
