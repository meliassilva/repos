namespace Byui.ApiSnippet.Business.Entities.Queries
{
    public abstract class BaseQueries
    {
        private readonly ApiSnippetContext _ctx;

        protected BaseQueries(ApiSnippetContext ctx)
        {
            _ctx = ctx;
        }
    }
}
