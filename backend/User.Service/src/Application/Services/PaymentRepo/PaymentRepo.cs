using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentRepo
{
    public class PaymentRepo:IPayment
    {
        private readonly DataContext _context;
        public PaymentRepo(DataContext context) 
        {
            _context = context;
        }

        public int GetPaymentsCount()
        {
            var result = _context.Payments.ToListAsync().Result.Count();
            return result;
        }


        public bool Save(string username)
        {
            Payment payment = new() 
            {
                Username= username,
            };
            _context.Payments.Add(payment);
            _context.SaveChanges();
            
            return true;
        }
    }
}
