using Codenation.Challenge.Models;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class PasswordValidatorService: IResourceOwnerPasswordValidator
    {
        public PasswordValidatorService(CodenationContext dbContext)
        {            
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var userName = context.UserName;
            var password = context.Password;

            context.Result = new GrantValidationResult( TokenRequestErrors.InvalidGrant, "Invalid username or password");


            return Task.CompletedTask;
        }
     
    }
}