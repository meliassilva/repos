using System;

namespace Byui.StudentListApi.Business.Utilities
{
    public interface IClock
    {
        DateTimeOffset Now { get; }
        DateTime Today { get; }
    }
}
