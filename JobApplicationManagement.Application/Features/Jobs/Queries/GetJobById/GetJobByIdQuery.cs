using JobApplicationManagement.Application.Dtos.JobDto;
using MediatR;

namespace JobApplicationManagement.Application.Features.Jobs.Queries.GetJobById
{
    public class GetJobByIdQuery : IRequest<JobResponseDto>
    {
        public int Id { get; set; }
    }
}
