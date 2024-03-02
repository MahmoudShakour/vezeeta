using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Models;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class DoctorSpecializationRepo : BaseRepo<DoctorSpecialization, int>, IDoctorSpecializationRepo
    {
        private readonly ApplicationDbContext _context;
        public DoctorSpecializationRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddDoctorSpecializations(string doctorId, List<Specialization> specializations)
        {
            foreach (var specialization in specializations)
            {
                var doctorSpecification = new DoctorSpecialization { DoctorId = doctorId, SpecializationId = specialization.Id };

                await _context.DoctorSpecializations.AddAsync(doctorSpecification);
                await _context.SaveChangesAsync();
                
            }
        }
    }
}