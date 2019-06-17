using System;

namespace Byui.Something.ApplicationCore.Common.Interfaces.Clock
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}