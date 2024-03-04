using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IAppointmentRepo:IBaseRepo<Appointment,int>
    {
        Task<Appointment?> Update(Appointment appointment);
    }
}