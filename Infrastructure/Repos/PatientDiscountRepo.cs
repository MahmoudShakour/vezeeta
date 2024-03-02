using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Models;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class PatientDiscountRepo : BaseRepo<PatientDiscount, int>, IPatientDiscountRepo
    {
        public PatientDiscountRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}