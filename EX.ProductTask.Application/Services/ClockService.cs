using System;
using Core.Interfaces.Common;
namespace Application.Services;
public class ClockService : IClockService
{
     public DateTime Now => DateTime.Now;
}
