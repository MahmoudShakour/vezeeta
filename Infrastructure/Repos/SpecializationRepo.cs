using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Models;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class SpecializationRepo : BaseRepo<Specialization, int>, ISpecializationRepo
    {
        private readonly ApplicationDbContext _context;
        public SpecializationRepo(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<List<Specialization>?> GetByNames(List<string> names)
        {
            List<Specialization> result = [];
            foreach (var name in names)
            {
                var Specialization=await base.FindOne(s=>s.Name==name);
                
                if (Specialization==null)
                    return null;
                
                result.Add(Specialization);
            }
            return result;
        }
    }
}