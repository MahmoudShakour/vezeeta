using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Helpers;
using Application.Interfaces.Repos;
using Core.Models;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJWTHelper _jWTHelper;

        public AuthRepo(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJWTHelper jWTHelper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTHelper = jWTHelper;
        }

        public async Task<string> CreateDoctor(ApplicationUser user, string password)
        {

            var createdUser = await _userManager.CreateAsync(user, password);
            if (!createdUser.Succeeded)
                return string.Empty;

            var result = await _userManager.AddToRoleAsync(user, "Doctor");
            if (!result.Succeeded)
                return string.Empty;


            return user.Id;
        }

        public async Task<string> CreatePatient(ApplicationUser user, string password)
        {
            var createdUser = await _userManager.CreateAsync(user, password);
            if (!createdUser.Succeeded)
                return string.Empty;

            var result = await _userManager.AddToRoleAsync(user, "Patient");
            if (!result.Succeeded)
                return string.Empty;

            return user.Id;
        }

        public async Task<ApplicationUser?> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> IsDuplicate(string email, string username)
        {
            return await _userManager.FindByEmailAsync(email) != null ||
                    await _userManager.FindByNameAsync(username) != null;
        }

        public async Task<string?> LoginUser(string username, string password)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                return null;


            var checkPassword = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!checkPassword.Succeeded)
                return null;


            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null || roles.Count != 1)
                return null;


            var token = _jWTHelper.GenerateToken(user.Email!, user.Id, roles[0]);
            return token;
        }
    }
}