using EduHub.API.Models;
using EudHub.API.Models;

namespace EudHub.API.Services
{
    public interface IFeedBackService
    {
        Task<IEnumerable<Feedback>> GetFeedbackAsync();
        Task<Feedback> SaveFeedback(Feedback saveFeedback);
        public int GetusermaxId();


    }
}
