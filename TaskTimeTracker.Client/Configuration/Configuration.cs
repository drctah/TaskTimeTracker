using System;
using System.Windows.Input;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Configuration {
  public class Configuration : ITaskTimeTrackerConfiguration {
    public Version Version { get; set; }
    public Key KeyOne { get; set; }
    public bool ControlIsChecked { get; set; }
    public bool AltIsChecked { get; set; }
    public bool WindowsIsChecked { get; set; }
    public bool SetStampOnStartupIsChecked { get; set; }
    public string StartupStampText { get; set; }

    public int CompareTo(ITaskTimeTrackerConfiguration other) {
      int result = this.KeyOne.CompareTo(other.KeyOne);
      if (result != 0) {
        return result;
      }

      result = this.ControlIsChecked.CompareTo(other.ControlIsChecked);
      if (result != 0) {
        return result;
      }

      result = this.AltIsChecked.CompareTo(other.AltIsChecked);
      if (result != 0) {
        return result;
      }

      result = this.WindowsIsChecked.CompareTo(other.WindowsIsChecked);
      if (result != 0) {
        return result;
      }

      result = this.SetStampOnStartupIsChecked.CompareTo(other.SetStampOnStartupIsChecked);
      if (result != 0) {
        return result;
      }

      result = String.Compare(this.StartupStampText, other.StartupStampText, StringComparison.Ordinal);
      return result;
    }
  }
}
