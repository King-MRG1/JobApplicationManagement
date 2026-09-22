using JobApplicationManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user, IList<string> roles, int? recruiterId = null, int? candidateId = null);
    }
}
