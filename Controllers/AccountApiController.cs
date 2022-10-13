namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecureSending.DTO;
    using SecureSending.Services.Account;

    // Authorized Controller !!!
    [ApiController]
    [Route("api/account")]
    public class AccountApiController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountApiController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterAccount(CredentialsDto credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var key = await _accountService.RegisterAccountAsync(credentials);

            return Ok(key);
        }
    }
}