using Common.DataContexts;
using Core.Interfaces;
using Core.Entities;

namespace Sigma_Software_Task.Repositories
{
    public class CSVCandidatesRepository : ICandidateRepository
    {
        protected CSVContext _csvContext;
        public CSVCandidatesRepository(DataContextsHub dataContextsHub)
        {
            _csvContext = dataContextsHub.csvContext;
        }
        public async Task<Candidate?> GetCandidateByEmailAddress(string emailAddress)
        {
            return await _csvContext.GetCandidateByEmailAddress(emailAddress);
        }
        public async Task<int> InsertOrUpdateCandidate(Candidate candidate, bool saveOnServer = false)
        {
            return await _csvContext.InsertOrUpdateCSVCandidate(candidate,saveOnServer);
        }
    }
}
