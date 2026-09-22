using JobApplicationManagement.Domain.Entities;
using MediatR;

namespace JobApplicationManagement.Application.Features.Recruiters.Queries.GetRecruiterById
{
    public class GetRecruiterByIdQuery : IRequest<Recruiter?>
    {
        public int Id { get; set; }
    }
}
