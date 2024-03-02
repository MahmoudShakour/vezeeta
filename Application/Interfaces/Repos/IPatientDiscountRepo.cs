using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IPatientDiscountRepo : IBaseRepo<PatientDiscount, int>
    {
        
    }
}