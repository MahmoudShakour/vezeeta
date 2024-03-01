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
            System.Console.WriteLine(specializations.Count);
            System.Console.WriteLine(specializations.Count);
            System.Console.WriteLine(specializations.Count);
            System.Console.WriteLine(specializations.Count);
            foreach (var specialization in specializations)
            {
                var doctorSpecification=new DoctorSpecialization { DoctorId = doctorId, SpecializationId = specialization.Id };
                System.Console.WriteLine(doctorSpecification.DoctorId);
                System.Console.WriteLine(doctorSpecification.SpecializationId);
                await _context.DoctorSpecializations.AddAsync(
                    doctorSpecification
                );
                await _context.SaveChangesAsync();
            }
        }
    }
}