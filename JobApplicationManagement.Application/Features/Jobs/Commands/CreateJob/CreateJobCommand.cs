using MediatR;

namespace JobApplicationManagement.Application.Features.Jobs.CreateJob
{
    public class CreateJobCommand : IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int recruiterId { get; set; }
    }
}
