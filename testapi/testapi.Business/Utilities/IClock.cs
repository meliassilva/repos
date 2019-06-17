using System;

namespace Byui.testapi.Business.Utilities
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}
