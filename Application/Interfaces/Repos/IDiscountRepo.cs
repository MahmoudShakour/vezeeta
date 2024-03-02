using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;

namespace Application.Interfaces.Repos
{
    public interface IDiscountRepo:IBaseRepo<Discount,int>
    {
        Task<Discount?> Activate(int discountId);
        Task<Discount?> Deactivate(int discountId);
        Task<Discount?> ChangePercent(int discountId,int percent);
    }
}