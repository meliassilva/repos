using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Byui.Something.ApplicationCore.Examples.Interactors.GetExample
{
    public class GetExampleInteractor : IRequestHandler<GetExampleRequest, List<string>>
    {

        public GetExampleInteractor()
        {
        }

        public async Task<List<string>> Handle(GetExampleRequest request, CancellationToken token)
        {
            var temp = new List<string> { "value 1", "value 2" };
            return await Task.FromResult(temp);
        }
    }
}