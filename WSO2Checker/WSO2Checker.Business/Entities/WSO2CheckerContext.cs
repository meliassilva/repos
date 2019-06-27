using Microsoft.EntityFrameworkCore;

namespace Byui.WSO2Checker.Business.Entities
{
    public class WSO2CheckerContext : DbContext
    {

        public WSO2CheckerContext(DbContextOptions<WSO2CheckerContext> options) : base(options)
        {
            
        }
        
    }
}
