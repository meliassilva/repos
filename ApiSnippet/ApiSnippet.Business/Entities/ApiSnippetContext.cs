using Microsoft.EntityFrameworkCore;

namespace Byui.ApiSnippet.Business.Entities
{
    public class ApiSnippetContext : DbContext
    {

        public ApiSnippetContext(DbContextOptions<ApiSnippetContext> options) : base(options)
        {
            
        }
        
    }
}
