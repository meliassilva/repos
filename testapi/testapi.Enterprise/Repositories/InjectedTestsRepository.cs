namespace Byui.testapi.Enterprise.Repositories
{
    ///// <summary>
    ///// This is an example implementation of a repository.  The person service is injected into it.  You want to take this approach when you
    ///// want to do any of the following:
    ///// 1. Unit Test your repository, so that you can Mock the SOA web services
    ///// 2. Control when the service clients are instantiated and disposed (you may want to use the same
    ///// service client for multiple calls)
    ///// </summary>
    //public class InjectedExampleRepository : IExampleRepository
    //{
    //    private readonly IPersonService _personService;

    //    public InjectedExampleRepository(IPersonService personService)
    //    {
    //        _personService = personService;
    //    }

    //    public List<Person> GetEmployees()
    //    {
    //        var personFilter = new PersonFilter()
    //        {
    //            IncludeOnlyEmployees = true,
    //            IncludeOnlyActive = false
    //        };
    //        return _personService.GetFilteredPersons(personFilter, new StudentFilter());
    //    }
    //}
}
