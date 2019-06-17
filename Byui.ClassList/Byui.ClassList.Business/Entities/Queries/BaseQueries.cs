namespace Byui.Byui.ClassList.Business.Entities.Queries
{
    public abstract class BaseQueries
    {
        private readonly Byui.ClassListContext _ctx;

        protected BaseQueries(Byui.ClassListContext ctx)
        {
            _ctx = ctx;
        }
    }
}
