using BlogWebAPI.Models.Users;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogWebAPI
{
    public class AppOauthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var verifiedUser = new UserAuthentication().AuthenticateUser(context.UserName, context.Password);

            if (verifiedUser != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Authorized_User"));
                identity.AddClaim(new Claim("userID", verifiedUser.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, verifiedUser.Username));
                identity.AddClaim(new Claim(ClaimTypes.Email, verifiedUser.Email));

                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_grant", "Provided data is incorrect");
                return;
            }

        }
    }
}