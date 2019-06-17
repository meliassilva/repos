using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Byui.Something.Api.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name => $"{FirstName} {LastName}";

        /// <summary>
        /// Inumber
        /// </summary>
        public string Inumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Roles
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        /// contructor
        /// </summary>
        public UserDto()
        {
            
        }

        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="claims"></param>
        public UserDto(List<Claim> claims)
        {
            Inumber = claims.FirstOrDefault(c => c.Type == Constants.Sub)?.Value;
            Email = claims.FirstOrDefault(c => c.Type == Constants.Email)?.Value;
            FirstName = claims.FirstOrDefault(c => c.Type == Constants.RestOfName)?.Value;
            LastName = claims.FirstOrDefault(c => c.Type == Constants.Surname)?.Value;
        }
    }
}