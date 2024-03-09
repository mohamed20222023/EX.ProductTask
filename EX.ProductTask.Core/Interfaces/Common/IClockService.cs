using System;

namespace Core.Interfaces.Common;
public interface IClockService
{
    DateTime Now { get; }
}
