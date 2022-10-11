namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using Services.Security;

    // Authorized Controller !!!
    [ApiController]
    [Route("/api")]
    public class UsersApiController : Controller
    {
        private readonly IGenerateSecureLink _generateSecureLink;
        private readonly IDbRepository _usersRepository;

        public UsersApiController(
            IDbRepository repository, 
            IGenerateSecureLink generateSecureLink)
        {
            _usersRepository = repository;
            _generateSecureLink = generateSecureLink;
        }

        [HttpGet]
        [Route("get/users")]
        public async Task<IActionResult> Get()
        {
            var users = await _usersRepository.GetAllAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet]
        [Route("generateUniqueLink")]
        public async Task<IActionResult> GenerateUniqueLink(string id) 
        { 
            var user = await _usersRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var generatedLinkExtension = _generateSecureLink.GetSecureExtension();

            var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            var location = $"{baseUrl}Credentials/Get/{generatedLinkExtension}";

            user.Link = location;

            _usersRepository.Save();

            return Ok(user);
        }
    }
}