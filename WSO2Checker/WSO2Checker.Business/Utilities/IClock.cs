using System;

namespace Byui.WSO2Checker.Business.Utilities
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}
