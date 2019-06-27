using System.Collections.Generic;
using Byui.ApiSnippet.Enterprise.Interfaces;

namespace Byui.ApiSnippet.Business.Business
{
    public class ExampleBusiness
    {
        private readonly IExampleRepository _exampleRepository;

        public ExampleBusiness(IExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public List<string> GetValues()
        {
            var temp = new List<string> { "value 1", "value 2" };
            return temp;
        }
    }
}
