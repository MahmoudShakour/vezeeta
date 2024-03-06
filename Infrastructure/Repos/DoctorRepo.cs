using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Models;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repos
{
    public class DoctorRepo : BaseRepo<Doctor, string>, IDoctorRepo
    {
        private readonly ApplicationDbContext _context;
        public DoctorRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<(string, int)>> GetTop(int numberOfDoctors)
        {
            var doctorsCount =
                await (
                    from b in _context.Bookings
                    join a in _context.Appointments on b.AppointmentId equals a.Id
                    group a by a.DoctorId into g
                    select new { DoctorId = g.Key, Count = g.Count() }
                )
                .OrderByDescending(x => x.Count)
                .Take(numberOfDoctors)
                .ToListAsync();

            return doctorsCount.Select(dc => (dc.DoctorId, dc.Count)).ToList();
        }

        public async Task<Doctor?> Update(Doctor doctor)
        {
            var doctorToUpdate = await FindOne(d => d.Id == doctor.Id, ["User"]);
            if (doctorToUpdate == null)
                return null;

            doctorToUpdate.Fees = doctor.Fees;
            doctorToUpdate.Gender = doctor.Gender;
            doctorToUpdate.User.PhoneNumber = doctor.User.PhoneNumber;

            return doctorToUpdate;
        }


    }
}