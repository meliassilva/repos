using Microsoft.EntityFrameworkCore;

namespace Byui.StudentListApi.Business.Entities
{
    public class StudentListApiContext : DbContext
    {

        public StudentListApiContext(DbContextOptions<StudentListApiContext> options) : base(options)
        {
            
        }
        
    }
}
