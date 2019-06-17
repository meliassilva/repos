namespace Byui.testapi.Business.Entities.Queries
{
    public abstract class BaseQueries
    {
        private readonly testapiContext _ctx;

        protected BaseQueries(testapiContext ctx)
        {
            _ctx = ctx;
        }
    }
}
