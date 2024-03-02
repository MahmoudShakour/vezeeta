using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface ISpecializationRepo:IBaseRepo<Specialization,int>
    {
        Task<List<Specialization>?> GetByNames(List<string> names); 
    }
}