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
    public class DiscountRepo : BaseRepo<Discount, int>, IDiscountRepo
    {
        private readonly ApplicationDbContext _context;
        public DiscountRepo(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Discount?> Activate(int discountId)
        {
            var discount= await _context.Discounts.FindAsync(discountId);
            if(discount == null)
                return null;

            discount.Status=DiscountStatus.Activated;
            await _context.SaveChangesAsync();

            return discount;
        }

        public async Task<Discount?> ChangePercent(int discountId, int percent)
        {
            var discount= await _context.Discounts.FindAsync(discountId);
            if(discount == null)
                return null;
            
            discount.Percentage=percent;
            await _context.SaveChangesAsync();

            return discount;
        }

        public async Task<Discount?> Deactivate(int discountId)
        {
            var discount= await _context.Discounts.FindAsync(discountId);
            if(discount == null)
                return null;

            discount.Status=DiscountStatus.Deactivated;
            await _context.SaveChangesAsync();

            return discount;
        }
    }
}