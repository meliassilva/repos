using Microsoft.EntityFrameworkCore;

namespace Byui.repos.Business.Entities
{
    public class reposContext : DbContext
    {

        public reposContext(DbContextOptions<reposContext> options) : base(options)
        {
            
        }
        
    }
}
