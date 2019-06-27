namespace Byui.WSO2Checker.Business.Entities.Queries
{
    public abstract class BaseQueries
    {
        private readonly WSO2CheckerContext _ctx;

        protected BaseQueries(WSO2CheckerContext ctx)
        {
            _ctx = ctx;
        }
    }
}
