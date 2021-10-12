using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace LicenceApi
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Log4net.createlog("AuthorizationServerProvider", "Task start");
            context.Validated();
            // Log4net.createlog("AuthorizationServerProvider", "Task finish");
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                using (UserAuthentication OBJ = new UserAuthentication())
                {
                    // Log4net.createlog("GrantResourceOwnerCredentials", "Start");
                    var user = OBJ.ValidateUser(context.UserName, context.Password);
                    // Log4net.createlog("Access user", user);
                    if (user == "false")
                    {
                        context.SetError("invalid_grant", "Username or password is incorrect");
                        // Log4net.createlog("invalid_grant", "Username or password is incorrect");
                        return;
                    }
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Role, "SuperAdmin"));
                    identity.AddClaim(new Claim(ClaimTypes.Name, "SuperAdmin"));
                    //identity.AddClaim(new Claim("Email", user.UserEmailID));
                    // Log4net.createlog("Add identity", "Finish auth");
                    context.Validated(identity);
                }
            }
            catch(Exception ex)
            {
                // Log4net.createlog("GrantResourceOwnerCredentials", ex.InnerException.ToString());
            }
        }
    }

}