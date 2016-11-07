using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskTimeTracker {
  internal class Task : INotifyPropertyChanged {
    private DateTime _creationTime;
    private string _tag;

    public Task(DateTime now, string text) {
      CreationTime = now;
      Tag = text;
    }

    public DateTime CreationTime {
      get { return _creationTime; }
      set {
        _creationTime = value;
        OnPropertyChanged();
      }
    }

    public string Tag {
      get { return _tag; }
      set {
        _tag = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}