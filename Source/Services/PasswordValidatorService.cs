using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService : IResourceOwnerPasswordValidator
    {
        private readonly CodenationContext _context;

        public PasswordValidatorService(CodenationContext dbContext)
        {
            _context = dbContext;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == context.UserName);

            var invalidUser = user == null || user.Password != context.Password;

            if (invalidUser)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
                return Task.CompletedTask;
            }

            var claims = UserProfileService.GetUserClaims(user);
            context.Result = new GrantValidationResult(user.Id.ToString(), "custom", claims);

            return Task.CompletedTask;
        }

    }
}