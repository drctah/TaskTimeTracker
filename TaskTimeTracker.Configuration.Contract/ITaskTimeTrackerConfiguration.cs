using System;
using System.Windows.Input;
using Base.Configuration.Contract.Configuration;

namespace TaskTimeTracker.Configuration.Contract {
  public interface ITaskTimeTrackerConfiguration : IConfiguration, IComparable<ITaskTimeTrackerConfiguration> {
    Key KeyOne { get; set; }
    bool ControlIsChecked { get; set; }
    bool AltIsChecked { get; set; }
    bool WindowsIsChecked { get; set; }

    bool SetStampOnStartupIsChecked { get; set; }
    string StartupStampText { get; set; }

    bool SetStampOnLockIsChecked { get; set; }
    string ScreenLockedText { get; set; }
    string ScreenUnlockedText { get; set; }

  }
}