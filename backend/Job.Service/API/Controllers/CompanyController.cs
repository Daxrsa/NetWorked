using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CompanyController:ControllerBase
    {
        private readonly ICompanyRepo _contract;
        public CompanyController(ICompanyRepo contract) 
        {
            _contract= contract;
        }

        /*[HttpGet]
        public */
    }
}
