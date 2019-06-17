using System;
using Byui.Something.ApplicationCore.Common.Interfaces.Clock;

namespace Byui.Something.Infrastructure.Common
{
    public class Clock : IClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;

        public DateTime Today => DateTime.Today;
    }
}