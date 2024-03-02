using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repos;
using Core.Enums;
using Core.Models;
using Infrastructure.Database;

namespace Infrastructure.Repos
{
    public class BookingRepo : BaseRepo<Booking, int>, IBookingRepo
    {
        private readonly ApplicationDbContext _context;
        public BookingRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Booking?> Cancel(int bookingId)
        {
            var booking=await GetById(bookingId); 
            if(booking==null)
                return null;
            
            booking.Status=BookingStatus.Cancelled;
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task<Booking?> Confirm(int bookingId)
        {
            var booking=await GetById(bookingId); 
            if(booking==null)
                return null;
            
            booking.Status=BookingStatus.Confirmed;
            await _context.SaveChangesAsync();
            return booking;
        }
    }
}