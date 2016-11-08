using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Ui.Commands;

namespace TaskTimeTracker.Client {
  public class Task : INotifyPropertyChanged, ITask {
    private EditableDateTime _creationTime;
    private string _tag;
    private TaskValue _valueBackup;
    private bool _editMode;
    public ICommand EditCommand { get; set; }

    public ICommand SaveCommand { get; set; }

    public ICommand AbortCommand { get; set; }

    public bool EditMode {
      get { return this._editMode; }
      set {
        this._editMode = value;
        this.OnPropertyChanged();
      }
    }

    public EditableDateTime CreationTime {
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

    public Task(DateTime now, string text) {
      this.CreationTime = now;
      this.Tag = text;
      this.EditCommand = new RelayCommand(EditCommandExecute);
      this.AbortCommand = new RelayCommand(AbortCommandExecute);
      this.SaveCommand = new RelayCommand(SaveCommandExecute);
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
      this.EditMode = true;
      this._valueBackup = new TaskValue(this);
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}