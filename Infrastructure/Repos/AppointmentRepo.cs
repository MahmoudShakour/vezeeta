using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Models;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class AppointmentRepo : BaseRepo<Appointment, int>, IAppointmentRepo
    {
        private readonly ApplicationDbContext _context;
        public AppointmentRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Appointment?> Update(Appointment appointment)
        {
            var appointmentToUpdate=await GetById(appointment.Id);
            if(appointmentToUpdate==null){
                return null;
            }

            appointmentToUpdate.Date=appointment.Date;

            return appointmentToUpdate;
        }
    }
}