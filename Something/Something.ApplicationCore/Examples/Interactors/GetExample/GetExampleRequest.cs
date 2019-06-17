using System;
using System.Collections.Generic;
using MediatR;

namespace Byui.Something.ApplicationCore.Examples.Interactors.GetExample
{
    public class GetExampleRequest : IRequest<List<string>>
    {        
        public GetExampleRequest()
        {
        }
    }
}