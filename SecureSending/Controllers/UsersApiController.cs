namespace SecureSending.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services.Data;
    using Services.Security;

    // Authorized Controller !!!
    [ApiController]
    [Route("/api")]
    public class UsersApiController : ControllerBase
    {
        private readonly IGenerateSecureLink _generateSecureLink;
        private readonly IDbService _usersRepository;

        public UsersApiController(
            IDbService repository, 
            IGenerateSecureLink generateSecureLink)
        {
            _usersRepository = repository;
            _generateSecureLink = generateSecureLink;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
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
        public async Task<IActionResult> GenerateUniqueLink(string username, string password) 
        { 
            var user = await _usersRepository.GetByUsernameAndPassAsync(username, password);

            if (user == null)
            {
                return NotFound();
            }

            var generatedLinkExtension = _generateSecureLink.GetSecureExtension();

            //var baseUrl = $"{Request.Scheme}://{Request.Host.Value}/";

            //var location = $"{baseUrl}Credentials/Index/{generatedLinkExtension}";

            user.Link = generatedLinkExtension;

            await _usersRepository.Save();

            return Ok(user);
        }
    }
}