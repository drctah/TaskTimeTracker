using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Ui.Commands;

namespace TaskTimeTracker.Client {
  public class Task : INotifyPropertyChanged, ITask {
    private Data.Task _valueBackup;
    private Data.Task _value;
    private bool _editMode;

    public ICommand EditCommand { get; set; }

    public ICommand SaveCommand { get; set; }

    public ICommand AbortCommand { get; set; }

    public ICommand CopyCommand { get; set; }

    public bool EditMode {
      get { return this._editMode; }
      set {
        this._editMode = value;
        this.OnPropertyChanged();
      }
    }

    public EditableDateTime CreationTime {
      get { return this._value.CreationTime; }
      set {
        this._value.CreationTime = value;
        OnPropertyChanged();
      }
    }

    public string Tag {
      get { return this._value.Tag; }
      set {
        this._value.Tag = value;
        OnPropertyChanged();
      }
    }

    public Task(DateTime now, string text) {
      this._value = new Data.Task(now, text);
      this.EditCommand = new RelayCommand(EditCommandExecute);
      this.AbortCommand = new RelayCommand(AbortCommandExecute);
      this.SaveCommand = new RelayCommand(SaveCommandExecute);
      this.CopyCommand = new RelayCommand(CopyExecute);
    }

    private void CopyExecute(object o) {
      Clipboard.SetText(this.Tag);
    }

    private void SaveCommandExecute(object obj) {
      this.EditMode = false;
    }

    private void AbortCommandExecute(object obj) {
      this.EditMode = false;
      this.Tag = this._valueBackup.Tag;
      this.CreationTime = this._valueBackup.CreationTime;
    }

    private void EditCommandExecute(object o) {
      this.EnterEditMode();
    }

    public void EnterEditMode() {
      this._valueBackup = new Data.Task(this._value);
      this.EditMode = true;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public bool Equals(ITaskTimeTrackerContractObject<ITask> other)
    {
      return this._value.Equals(other);
    }

    public int CompareTo(ITaskTimeTrackerContractObject<ITask> other)
    {
      return this._value.CompareTo(other);
    }

    public int CompareTo(object obj)
    {
      return this._value.CompareTo(obj);
    }

    public object Clone()
    {
      return this._value.Clone();
    }
  }
}
