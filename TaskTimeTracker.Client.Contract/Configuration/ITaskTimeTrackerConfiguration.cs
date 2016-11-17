using System;
using System.Windows.Input;
using Base.Configuration.Contract.Configuration;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface ITaskTimeTrackerConfiguration : IConfiguration, IComparable<ITaskTimeTrackerConfiguration> {
    Key KeyOne { get; set; }
    bool ControlIsChecked { get; set; }
    bool AltIsChecked { get; set; }
    bool WindowsIsChecked { get; set; }
    bool SetStampOnStartupIsChecked { get; set; }
    string StartupStampText { get; set; }
  }
}