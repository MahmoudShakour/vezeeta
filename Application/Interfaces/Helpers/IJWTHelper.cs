using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Helpers
{
    public interface IJWTHelper
    {
        string GenerateToken(string email, string userId, string roleName);
        ITokenInfo? DecodeToken();
    }
}