namespace JobApplicationManagement.Application.Interfaces
{
    /// <summary>
    /// Exposes the current authenticated user's recruiter context.
    /// Implemented in Infrastructure via IHttpContextAccessor so the
    /// Application layer stays free of ASP.NET Core types.
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>The recruiter id embedded in the JWT, or null if the user is not a recruiter.</summary>
        int? RecruiterId { get; }
        /// <summary>The raw IdentityUser id (sub claim) from the JWT.</summary>
        string? CandidateId { get; }
    }
}