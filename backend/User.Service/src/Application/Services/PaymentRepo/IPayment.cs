using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PaymentRepo
{
    public interface IPayment
    {
        bool Save(string username);
        int GetPaymentsCount();
    }
}
