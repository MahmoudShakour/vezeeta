using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IDoctorRepo:IBaseRepo<Doctor,string>
    {
        Task<Doctor?> Update(Doctor doctor);
    }
}