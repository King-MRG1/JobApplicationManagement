using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplicationManagement.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Domain.Entities.ApplicationUser user, IList<string> roles);
    }
}
