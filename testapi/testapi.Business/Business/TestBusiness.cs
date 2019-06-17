using System.Collections.Generic;
using Byui.testapi.Enterprise.Interfaces;

namespace Byui.testapi.Business.Business
{
    public class TestBusiness
    {
        private readonly ITestRepository _testRepository;

        public TestBusiness(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public List<string> GetValues()
        {
            var temp = new List<string> { "value 1", "value 2" };
            return temp;
        }
    }
}
