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
        public AppointmentRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}