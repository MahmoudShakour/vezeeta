using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interfaces.Repos
{
    public interface IUnitOfWork : IDisposable
    {
        ISpecializationRepo Specializations { get; }
        IDoctorSpecializationRepo DoctorSpecializations { get; }

        Task Complete();
    }
}