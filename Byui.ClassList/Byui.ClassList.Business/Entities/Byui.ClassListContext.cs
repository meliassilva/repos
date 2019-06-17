using Microsoft.EntityFrameworkCore;

namespace Byui.Byui.ClassList.Business.Entities
{
    public class Byui.ClassListContext : DbContext
    {

        public Byui.ClassListContext(DbContextOptions<Byui.ClassListContext> options) : base(options)
        {
            
        }
        
    }
}
