namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecureSending.Services.Account;

    public class CredentialsController : Controller
    {
        private readonly IAccountService _accountService;

        public CredentialsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var credentials = await _accountService.GetCredentialsByKey(id);

            if (credentials == null)
            {
                return NotFound();
            }

            return View(credentials);
        }
    }
}
