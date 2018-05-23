using Microsoft.AspNetCore.Identity;

namespace ExploreBooks.Extensions
{
    public class IdentityErrorDescriber : Microsoft.AspNetCore.Identity.IdentityErrorDescriber
    {
        public override IdentityError InvalidEmail(string email) {  return new IdentityError { Code = nameof(InvalidEmail), Description = $"Email seems incorrect.."};}
    }
}
