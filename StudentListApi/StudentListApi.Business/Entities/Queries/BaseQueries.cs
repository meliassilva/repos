namespace Byui.StudentListApi.Business.Entities.Queries
{
    public abstract class BaseQueries
    {
        private readonly StudentListApiContext _ctx;

        protected BaseQueries(StudentListApiContext ctx)
        {
            _ctx = ctx;
        }
    }
}
