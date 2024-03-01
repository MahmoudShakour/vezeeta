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

        public async Task<bool> CreateDoctor(Doctor doctor, string password)
        {
            var createdUser = await _userManager.CreateAsync(doctor.User, password);
            if (!createdUser.Succeeded)
                return false;

            var result = await _userManager.AddToRoleAsync(doctor.User, "Doctor");
            if (!result.Succeeded)
                return false;

            await _context.Doctors.AddAsync(doctor);
            return true;
        }

        public async Task<bool> CreatePatient(Patient patient, string password)
        {
            var createdUser = await _userManager.CreateAsync(patient.User, password);
            if (!createdUser.Succeeded)
                return false;

            var result = await _userManager.AddToRoleAsync(patient.User, "Patient");
            if (!result.Succeeded)
                return false;

            await _context.Patients.AddAsync(patient);
            return true;
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