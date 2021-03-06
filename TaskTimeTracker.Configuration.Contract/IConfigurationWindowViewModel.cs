﻿using System.ComponentModel;
using System.Windows.Input;

namespace TaskTimeTracker.Configuration.Contract {
  public interface IConfigurationWindowViewModel : INotifyPropertyChanged {
    string KeyOneString { get; set; }
    Key KeyOne { get; set; }
    bool ControlIsChecked { get; set; }
    bool AltIsChecked { get; set; }
    bool WindowsIsChecked { get; set; }

    bool SetStampOnStartupIsChecked { get; set; }
    string StartupStampText { get; set; }

    ICommand CancelCommand { get; }
    ICommand OkCommand { get; }
    IConfigurationViewModelController Controller { get; set; }

    bool SetStampOnLockIsChecked { get; set; }
    string ScreenLockedText { get; set; }
    string ScreenUnlockedText { get; set; }
  }
}