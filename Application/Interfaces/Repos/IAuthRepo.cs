using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IAuthRepo
    {
        Task<bool> IsDuplicate(string email, string username);
        Task<string> CreateDoctor(ApplicationUser user, string password);
        Task<string> CreatePatient(ApplicationUser user, string password);
        Task<string?> LoginUser(string username, string password);
        Task<ApplicationUser?> GetUserById(string id);
    }
}