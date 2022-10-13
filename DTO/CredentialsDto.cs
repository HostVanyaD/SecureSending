namespace SecureSending.DTO
{
    using System.ComponentModel.DataAnnotations;

    public class CredentialsDto
    {
        [Required]
        [StringLength(
             20,
             MinimumLength = 6,
             ErrorMessage = "Username must be between {2} and {1} characters long.")]
        public string Username { get; set; }

        [Required]
        [StringLength(
             50,
             MinimumLength = 6,
             ErrorMessage = "Username must be between {2} and {1} characters long.")]
        public string Password { get; set; }
    }
}
