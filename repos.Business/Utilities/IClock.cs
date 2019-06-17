using System;

namespace Byui.repos.Business.Utilities
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}
