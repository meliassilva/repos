using System;

namespace Byui.StudentListApi.Business.Utilities
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
        public DateTime Today => DateTime.Today;
    }
}
