using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IDoctorSpecializationRepo:IBaseRepo<DoctorSpecialization,int>
    {
        Task AddDoctorSpecializations(string doctorId,List<Specialization> specializations);
    }
}