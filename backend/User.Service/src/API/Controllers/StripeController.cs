using Application.Services.Contracts;
using Application.Services.PaymentRepo;
using Domain.Models.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class StripeController : Controller
    {
        private readonly IStripeAppService _stripeService;
        private readonly IChangeRole _user;
        private readonly IPayment _payment;

        public StripeController(IStripeAppService stripeService, IChangeRole user, IPayment payment)
        {
            _stripeService = stripeService;
            _user = user;
            _payment= payment;
        }

        [HttpPost("customer/add")]
        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer(
            [FromBody] AddStripeCustomer customer,
            CancellationToken ct)
        {
            StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(customer,ct);

            return StatusCode(StatusCodes.Status200OK, createdCustomer);
        }

        [HttpPost("payment/add")]
        [Authorize]
        public async Task<ActionResult<StripePayment>> AddStripePayment([FromBody] AddStripeId stripeId,CancellationToken ct)
        {
            var payment = new AddStripePayment
                (
                    stripeId.CustomerId,
                    "ft53961@ubt-uni.net",
                    "Payment to upgrade to recruiter role",
                    "USD",
                    5999
                );
            var user = _user.GetLoggedInUser();
            if (user.Role.Equals("Recruiter"))
            {
                return BadRequest("Role is already Recruiter");
            }
            await _user.ChangeUserRole(user.Id);
            _payment.Save(user.Username);
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment,ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }

        /*HttpGet]
        public IActionResult GetCount()
        {
            var result = _payment.GetPaymentsCount();
            return Ok(result);
        }*/
    }
}
