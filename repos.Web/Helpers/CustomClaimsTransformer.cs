//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;
//using IdentityModel;
//using Microsoft.AspNetCore.Authentication;

//namespace Byui.repos.Web.Helpers
//{
//    /// <summary>
//    /// ClaimsTransformer
//    /// </summary>
//    public class CustomClaimsTransformer : IClaimsTransformer
//    {

//        /// <summary>
//        /// TransformAsync
//        /// </summary>
//        /// <param name="context"></param>
//        /// <returns></returns>
//        public Task<ClaimsPrincipal> TransformAsync(ClaimsTransformationContext context)
//        {
//            var identity = (ClaimsIdentity)context.Principal.Identity;
//            var claims = identity.Claims.ToList();

//            var roles = claims.FirstOrDefault(c => c.Type == "role");
//            if (roles != null)
//            {
//                claims.Remove(roles);
//                claims.AddRange(roles.Value.Split(',').Select(r => new Claim(ClaimTypes.Role, r.Trim())));
//            }

//            var sub = claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject);
//            if (sub != null)
//            {
//                claims.Remove(sub);
//                sub = new Claim(ClaimTypes.Name, sub.Value);
//                claims.Add(sub);
//            }

//            var newIdentity = new ClaimsIdentity(claims, identity.AuthenticationType);
//            var principal = new ClaimsPrincipal(newIdentity);
//            return Task.FromResult(principal);
//        }


//    }
//}
