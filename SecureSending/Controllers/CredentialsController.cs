namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecureSending.Models;
    using Services.Data;

    [Route("/Credentials")]
    public class CredentialsController : Controller
    {
        private readonly DbRepository _repository;

        public CredentialsController(DbRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("get/{uniqueLink}")]
        public async Task<IActionResult> Get([FromRoute] string uniqueLink, int clicks = 1)
        {         

            if(clicks > 2)
            {
                return BadRequest();
            }

            var user = await _repository.GetUserCredentialsByUniqueLinkAsync(uniqueLink);

            if (user == null)
            {
                return NotFound();
            }

            var userCredentials = new CredentialsDto()
            {
                Username = user.Username,
                Password = user.Password
            };

            clicks++;

            return View(userCredentials);
        }
    }
}
