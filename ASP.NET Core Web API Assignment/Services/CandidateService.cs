using Common.DataContexts;
using Core.Interfaces;
using Core.Entities;
using Core.Dtos;

namespace Sigma_Software_Task.Repositories
{
    public class CandidateService : DBConfigGetService<Candidate>, ICandidateService
    {
        private ICandidateRepository _candidateRepository;
        public CandidateService(ICandidateRepository candidateRepository, IGetRepository<Candidate> getRepository) : base(getRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<ResponseModel> InsertOrUpdateCandidate(Candidate candidate, bool saveOnServer = false)
        {
            try
            {
                var res = await _candidateRepository.InsertOrUpdateCandidate(candidate,saveOnServer);
                if (res > 0)
                    return new ResponseModel() { Message = "Saved successfully", StatusCode = 200 };

                return new ResponseModel() { Message = "No rows affected", StatusCode = 400 };
            }
            catch (Exception ex)
            {
                return new ResponseModel() { Message = ex.InnerException!.Message, StatusCode = 500 };
            }
        }
        public async Task<Candidate?> GetCandidateByEmailAddress(string emailAddress)
        {
            return await _candidateRepository.GetCandidateByEmailAddress(emailAddress);
        }
    }
}
