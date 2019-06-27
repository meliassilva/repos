using System.Collections.Generic;

namespace Byui.WSO2Checker.Web.Helpers
{
    /// <summary>
    /// Helps us do application-specific roles; kind of poor man's policy.  We'll end up sending these roles down to the UI
    /// as part of the user object.  Remember, the UI can use these roles to help make a nice user experience, but ultimately
    /// your API needs to enforce all security via the WSO2 tokens.
    /// </summary>
    public class SecurityHelper
    {
        // TODO add in the BYU-I Roles that your application will translate to "application-specific" roles
        // BYU-Idaho Roles
        private const string SOFTWARE_ENGINEER = "Software Engineer";
        
        // Application Roles for the user interface to use; this way we don't "leak" our BYU-I roles down to the UI
        private const string ADMIN = "Admin";

        /// <summary>
        /// Converts the BYU-Idaho Roles to Application Roles.
        /// </summary>
        /// <param name="byuiRoles"></param>
        /// <returns></returns>
        public static List<string> ConvertByuiRolesToApplicationRoles(IEnumerable<string> byuiRoles)
        {
            var applicationRoles = new List<string>();
            foreach (string role in byuiRoles)
            {
                switch (role)
                {
                    case SOFTWARE_ENGINEER:
                        applicationRoles.Add(ADMIN);
                        break;
                }
            }

            return applicationRoles;
        }
    }
}
