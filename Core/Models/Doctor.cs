using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Models
{
    public class Doctor
    {
        public string Id { get; set; }=string.Empty;
        public ApplicationUser User { get; set; }
        public int Fees { get; set; }
        public List<Specialization> Specializations { get; set; }=[];
        public Gender Gender { get; set; }
    }
}