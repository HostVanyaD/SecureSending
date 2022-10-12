namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SecureSending.Models;
    using Services.Data;

    public class CredentialsController : Controller
    {
        private readonly IDbService _repository;

        public CredentialsController(IDbService repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index(string id)
        {
            //if(clicks > 2)
            //{
            //    return BadRequest();
            //}

            //var user = await _repository.GetUserByUniqueLinkAsync(id);

            //if (user == null)
            //{
            //    return NotFound("User Not Found");
            //}

            //var userCredentials = new CredentialsDto()
            //{
            //    Username = user.Username,
            //    Password = user.Password
            //};

            var userCredentials = new CredentialsDto()
            {
                Username = "Username",
                Password = "Password"
            };

            //clicks++;

            return this.View(userCredentials);
        }
    }
}
