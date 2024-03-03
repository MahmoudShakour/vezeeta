using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Helpers
{
    public interface ITokenInfo
    {
        public string Id { get; set; }
        public string Role { get; set; }
    }
}