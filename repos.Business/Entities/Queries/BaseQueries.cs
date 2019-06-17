namespace Byui.repos.Business.Entities.Queries
{
    public abstract class BaseQueries
    {
        private readonly reposContext _ctx;

        protected BaseQueries(reposContext ctx)
        {
            _ctx = ctx;
        }
    }
}
