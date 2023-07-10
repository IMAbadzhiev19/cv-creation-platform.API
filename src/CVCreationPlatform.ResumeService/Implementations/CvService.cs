using CVCreationPlatform.ResumeService.Contracts;
using CVCreationPlatform.ResumeService.Models;
using Data.Data;

namespace CVCreationPlatform.ResumeService.Implementations
{
    public class CvService : ICvService
    {
        private readonly ApplicationDbContext _context;

        public CvService(ApplicationDbContext context)
            => _context = context;

        public Task<bool> CreateResumeAsync(ResumeDTO resumeModel)
        {
            throw new NotImplementedException();
        }
        public Task<bool> UpdateResumeAsync(int oldResumeId, ResumeDTO newResumeModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteResumeAsync(int resumeId)
        {
            throw new NotImplementedException();
        }

        public Task<ResumeDTO> GetResumeByIdAsync(int resumeId)
        {
            throw new NotImplementedException();
        }

    }
}