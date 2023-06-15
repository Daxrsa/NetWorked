using Application.Services.Contracts;
using Domain.Models.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class StripeController : Controller
    {
        private readonly IStripeAppService _stripeService;
        private readonly IChangeRole role;

        public StripeController(IStripeAppService stripeService, IChangeRole role)
        {
            _stripeService = stripeService;
            this.role = role;
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
            var user = role.GetLoggedInUser();
            if (user.Role.Equals("Recruiter"))
            {
                return BadRequest("Role is already Recruiter");
            }
            await role.ChangeUserRole(user.Id);
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(payment,ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }
    }
}
