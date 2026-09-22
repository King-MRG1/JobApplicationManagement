using JobApplicationManagement.Application.Interfaces;
using JobApplicationManagement.Domain.Entities;
using JobApplicationManagement.Domain.Exceptions;
using MediatR;

namespace JobApplicationManagement.Application.Features.Jobs.CloseJob
{
    public class CloseJobHandler : IRequestHandler<CloseJobCommand, int>
    {
        private readonly IGenericRepository<Job> _jobRepository;
        public CloseJobHandler(IGenericRepository<Job> jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<int> Handle(CloseJobCommand request, CancellationToken cancellationToken)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId)
                ?? throw new DomainException($"Job with id {request.JobId} was not found.");
            if (job.RecruiterId != request.RecruiterId)
                throw new DomainException("You are not authorised to close this job.");
            job.IsActive = false;
            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();

            return job.Id;
        }
    }
}
