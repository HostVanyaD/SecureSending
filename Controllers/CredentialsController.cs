namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecureSending.Services.Account;
    using System.Net;

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

            return View(credentials);
        }
    }
}
