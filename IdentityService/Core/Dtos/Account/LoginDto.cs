namespace IdentityService.Core.DTOs.Account
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
