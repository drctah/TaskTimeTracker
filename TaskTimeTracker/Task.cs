using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client {
  public class Task : INotifyPropertyChanged, ITask {
    private DateTime _creationTime;
    private string _tag;

    public Task(DateTime now, string text) {
      this.CreationTime = now;
      this.Tag = text;
    }

    public DateTime CreationTime {
      get { return this._creationTime; }
      set {
        this._creationTime = value;
        OnPropertyChanged();
      }
    }

    public string Tag {
      get { return this._tag; }
      set {
        this._tag = value;
        OnPropertyChanged();
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}