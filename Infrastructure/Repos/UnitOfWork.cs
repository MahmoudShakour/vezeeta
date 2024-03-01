using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ISpecializationRepo Specializations { get; private set; }
        public IDoctorSpecializationRepo DoctorSpecializations { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Specializations = new SpecializationRepo(_context);
            DoctorSpecializations=new DoctorSpecializationRepo(_context);
        }

        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}