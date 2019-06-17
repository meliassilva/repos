using Microsoft.EntityFrameworkCore;

namespace Byui.testapi.Business.Entities
{
    public class testapiContext : DbContext
    {

        public testapiContext(DbContextOptions<testapiContext> options) : base(options)
        {
            
        }
        
    }
}
