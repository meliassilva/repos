using System;

namespace Byui.WSO2Checker.Business.Utilities
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
        public DateTime Today => DateTime.Today;
    }
}
