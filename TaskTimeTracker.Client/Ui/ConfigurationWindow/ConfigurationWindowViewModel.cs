using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskTimeTracker.Client.Ui.Commands;
using TaskTimeTracker.Configuration.Contract;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationWindowViewModel : IConfigurationWindowViewModel {
    private string _keyOneString;
    private Key _keyOne;
    private bool _controlIsChecked;
    private bool _altIsChecked;
    private bool _windowsIsChecked;
    private string _startupStampText;
    private bool _setStampOnStartupIsChecked;
    private bool _setStampOnLockIsChecked;
    private string _screenLockedText;
    private string _screenUnlockedText;

    public event PropertyChangedEventHandler PropertyChanged;

    public IConfigurationViewModelController Controller { get; set; }

    public bool SetStampOnStartupIsChecked {
      get { return this._setStampOnStartupIsChecked; }
      set {
        this._setStampOnStartupIsChecked = value;
        OnPropertyChanged(nameof(this.SetStampOnStartupIsChecked));
      }
    }


    public string StartupStampText {
      get { return this._startupStampText; }
      set {
        this._startupStampText = value;
        OnPropertyChanged(nameof(this.StartupStampText));
      }
    }

    public string KeyOneString {
      get { return this._keyOneString; }
      set {
        this._keyOneString = value;
        OnPropertyChanged(nameof(this.KeyOneString));
      }
    }

    public Key KeyOne {
      get { return this._keyOne; }
      set {
        this._keyOne = value;
        OnPropertyChanged(nameof(this.KeyOne));
      }
    }

    public bool ControlIsChecked {
      get { return this._controlIsChecked; }
      set {
        this._controlIsChecked = value;
        OnPropertyChanged(nameof(this.ControlIsChecked));
      }
    }

    public bool AltIsChecked {
      get { return this._altIsChecked; }
      set {
        this._altIsChecked = value;
        OnPropertyChanged(nameof(this.AltIsChecked));
      }
    }

    public bool WindowsIsChecked {
      get { return this._windowsIsChecked; }
      set {
        this._windowsIsChecked = value;
        OnPropertyChanged(nameof(this.WindowsIsChecked));
      }
    }

    public bool SetStampOnLockIsChecked {
      get { return this._setStampOnLockIsChecked; }
      set {
        this._setStampOnLockIsChecked = value;
        OnPropertyChanged(nameof(this.SetStampOnLockIsChecked));
      }
    }

    public string ScreenLockedText {
      get { return this._screenLockedText; }
      set {
        this._screenLockedText = value;
        OnPropertyChanged(nameof(this.ScreenLockedText));
      }
    }

    public string ScreenUnlockedText {
      get { return this._screenUnlockedText; }
      set {
        this._screenUnlockedText = value;
        OnPropertyChanged(nameof(this.ScreenUnlockedText));
      }
    }

    public ICommand CancelCommand { get; }

    public ICommand OkCommand { get; }


    public ConfigurationWindowViewModel(IConfigurationViewModelController controller) {
      this.Controller = controller;
      this.CancelCommand = new RelayCommand(controller.ExecuteCancel);
      this.OkCommand = new RelayCommand(controller.ExecuteOk);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
