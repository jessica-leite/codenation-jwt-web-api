using Codenation.Challenge.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Codenation.Challenge.Services
{
    public class UserProfileService : IProfileService
    {
        private readonly CodenationContext _context;
        public UserProfileService(CodenationContext dbContext)
        {
            _context = dbContext;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var request = context.ValidatedRequest as ValidatedTokenRequest;
            if (request != null)
            {
                var id = Convert.ToInt32(context.Subject.GetSubjectId());
                var user = _context.Users.Find(id);

                if (user != null)
                {
                    context.IssuedClaims = GetUserClaims(user).ToList();
                }
            }

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

        public static Claim[] GetUserClaims(User user)
        {
            return new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Email == "tegglestone9@blog.com" ? "Admin" : "User")
            };
        }

    }
}