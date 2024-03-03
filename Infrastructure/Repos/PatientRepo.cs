using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Core.Models;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class PatientRepo : BaseRepo<Patient, string>, IPatientRepo
    {
        public PatientRepo(ApplicationDbContext context) : base(context)
        {
        }
    }
}