using System;

namespace Byui.testapi.Business.Utilities
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
        public DateTime Today => DateTime.Today;
    }
}
