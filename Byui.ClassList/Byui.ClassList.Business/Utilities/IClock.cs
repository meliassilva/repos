using System;

namespace Byui.Byui.ClassList.Business.Utilities
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}
