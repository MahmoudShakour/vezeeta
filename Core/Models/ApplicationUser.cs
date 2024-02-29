using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class ApplicationUser:IdentityUser
    {
        public UserRole Role { get; set; }
    }
}