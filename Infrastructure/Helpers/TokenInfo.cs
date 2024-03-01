using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Helpers;

namespace Infrastructure.Helpers
{
    public class TokenInfo:ITokenInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}