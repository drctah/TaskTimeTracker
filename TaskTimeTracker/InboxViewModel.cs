using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TaskTimeTracker {
  class InboxViewModel : INotifyPropertyChanged {
    private readonly Window _window;
    private string _text;

    public ICommand OkCommand { get; set; }

    public ICommand AbortCommand { get; set; }

    public string Text {
      get { return _text; }
      set {
        _text = value;
        OnPropertyChanged();
      }
    }

    public InboxViewModel(Window window) {
      _window = window;
      OkCommand = new RelayCommand(OkCommandExecute);
      AbortCommand = new RelayCommand(AbortCommandExecute);
    }

    private void AbortCommandExecute(object o) {
      this._window.DialogResult = false;
      this._window.Close();
    }

    private void OkCommandExecute(object o) {
      this._window.DialogResult = true;
      this._window.Close();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
