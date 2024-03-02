using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IBookingRepo:IBaseRepo<Booking,int>
    {
        Task<Booking?> Confirm(int bookingId);   
        Task<Booking?> Cancel(int bookingId);   
    }
}