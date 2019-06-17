using System.Collections.Generic;

namespace Byui.Byui.ClassList.Web.Helpers
{
    public class User
    {
        public string Inumber { get; set; }
        public string Name { get; set; }
        public string RestOfName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<string> ByuiRoles { get; set; }
        public List<string> Roles { get; set; }
    }
}
