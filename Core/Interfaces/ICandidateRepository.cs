using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetCandidateByEmailAddress(string emailAddress);
        Task<int> InsertOrUpdateCandidate(Candidate candidate, bool saveOnServer = false);
    }
}