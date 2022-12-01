using Common.DataContexts;
using Core.Interfaces;
using Core.Entities;
using Core.Dtos;

namespace Sigma_Software_Task.Repositories
{
    public class CandidateRepository : DBConfigGetRepository<Candidate>, ICandidateRepository
    {
        private DBContext _dbContext;
        public CandidateRepository(DataContextsHub dataContextsHub):base(dataContextsHub)
        {
            _dbContext = dataContextsHub.dbContext;
        }
        public async Task<int> InsertOrUpdateCandidate(Candidate candidateDto, bool saveOnServer = false)
        {
            var candidate = _dbContext.Candidates.FirstOrDefault(c => c.Email == candidateDto.Email);

            if (candidate == null)
                await _dbContext.Candidates.AddAsync(new Candidate() {
                    Email = candidateDto.Email, FirstName = candidateDto.FirstName, LastName = candidateDto.LastName,
                    GitHubProfileUrl = candidateDto.GitHubProfileUrl, LinkedInProfileUrl = candidateDto.LinkedInProfileUrl,
                    PhoneNumber = candidateDto.PhoneNumber, TimeInterval = candidateDto.TimeInterval, TextComment = candidateDto.TextComment });
            else
            {
                candidate.Email = candidateDto.Email;
                candidate.FirstName = candidateDto.FirstName;
                candidate.LastName = candidateDto.LastName;
                candidate.PhoneNumber = candidateDto.PhoneNumber;
                candidate.TimeInterval = candidateDto.TimeInterval;
                candidate.TextComment = candidateDto.TextComment;
                candidate.GitHubProfileUrl = candidateDto.GitHubProfileUrl;
                candidate.LinkedInProfileUrl = candidateDto.LinkedInProfileUrl;
            }
            if (saveOnServer)
                return await _dbContext.SaveChangesAsync();

            return await Task.FromResult(1);
        }
        public Task<Candidate?> GetCandidateByEmailAddress(string emailAddress)
        {
            return base.GetById(emailAddress);
        }
    }
}
