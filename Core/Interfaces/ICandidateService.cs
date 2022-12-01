using Core.Dtos;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICandidateService
    {
        Task<Candidate?> GetCandidateByEmailAddress(string emailAddress);
        Task<ResponseModel> InsertOrUpdateCandidate(Candidate candidateDto, bool saveOnServer = false);
    }
}