using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Models;

namespace Application.Interfaces
{
    public interface IPatientRepo:IBaseRepo<Patient,string>
    {
    }
}