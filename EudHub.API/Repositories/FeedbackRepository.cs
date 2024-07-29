using BenchTask.API.Models;
using EudHub.API.Models;
using EudHub.API.Services;
using Microsoft.EntityFrameworkCore;

namespace EudHub.API.Repositories
{
    public class FeedbackRepository : IFeedBackService
    {
        private readonly EduHubInfoContext  _feedbackContext;
        public FeedbackRepository(EduHubInfoContext feedbackContext) 
        {
            _feedbackContext = feedbackContext;
        }


        public async Task<IEnumerable<Feedback>> GetFeedbackAsync()
        {
            return await _feedbackContext.Feedbacks.ToListAsync();
        }

        public async Task<Feedback> SaveFeedback(Feedback saveFeedback)
        {
            await _feedbackContext.Feedbacks.AddAsync(saveFeedback);
            await _feedbackContext.SaveChangesAsync();
            return saveFeedback;
        }

        public int GetusermaxId()
        {
            return _feedbackContext.Feedbacks.Max(u => (int?)u.feedbackId) ?? 0;
        }

    }
}
