using System;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfiguration : IComparable<IConfiguration> {
    Key KeyOne { get; set; }
    bool ControlIsChecked { get; set; }
    bool AltIsChecked { get; set; }
    bool WindowsIsChecked { get; set; }
  }
}