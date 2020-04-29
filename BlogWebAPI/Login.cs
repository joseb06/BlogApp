using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using BlogDatabase.Models;
using BlogWebAPI.Models;
using Microsoft.Owin.Security.OAuth;

namespace BlogWebAPI
{
    public class Login: OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            blogdbEntities blogdb = new blogdbEntities();
            
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var authenticatedUser = blogdb.users.Where(u => u.userName == context.UserName && u.password == context.Password);

            if (authenticatedUser.Count() > 0)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "Authenticated User"));
                identity.AddClaim(new Claim(ClaimTypes.Name, authenticatedUser.First().userName));
                identity.AddClaim(new Claim(ClaimTypes.Email, authenticatedUser.First().email));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Permiso Denegado, Datos erroneos");
                return;
            }

        }
    }
}