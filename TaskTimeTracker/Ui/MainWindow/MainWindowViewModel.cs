using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Ui.Commands;
using TaskTimeTracker.Client.Ui.Inbox;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  internal class MainWindowViewModel : IMainWindowViewModel {
    private ObservableCollection<ITask> _tasks;
    private ITask _selectedTask;
    private Visibility _mainWindowVisibility;

    public ObservableCollection<ITask> Tasks {
      get { return this._tasks; }
      set {
        this._tasks = value;
        OnPropertyChanged();
      }
    }

    public ITask SelectedTask {
      get { return this._selectedTask; }
      set {
        this._selectedTask = value;
        OnPropertyChanged();
      }
    }

    public Visibility MainWindowVisibility {
      get { return this._mainWindowVisibility; }
      set {
        this._mainWindowVisibility = value;
        OnPropertyChanged();
      }
    }

    /// <summary>
    /// Adds a TaskStamp
    /// </summary>
    public ICommand AddCommand { get; set; }

    /// <summary>
    /// Removes a TaskStamp
    /// </summary>
    public ICommand RemoveCommand { get; set; }

    /// <summary>
    /// Command to open the ConfigurationWindow
    /// </summary>
    public ICommand ConfigCommand { get; set; }

    public MainWindowViewModel(IEnumerable<ITask> tasks) {
      this.Tasks = new ObservableCollection<ITask>(tasks);
      this.AddCommand = new RelayCommand(AddExecute);
      this.RemoveCommand = new RelayCommand(RemoveExecute, o => this.SelectedTask != null);
      this.ConfigCommand = new RelayCommand(ConfigExecute);
      this.MainWindowVisibility = Visibility.Visible;
    }

    private void ConfigExecute(object obj) {
      throw new NotImplementedException();
    }

    private void RemoveExecute(object obj) {
      this.Tasks.Remove(this.SelectedTask);
    }

    private void AddExecute(object o) {
      Inbox.Inbox inbox = new Inbox.Inbox();
      InboxViewModel vm = new InboxViewModel(inbox);
      inbox.DataContext = vm;
      bool? b = inbox.ShowDialog();

      if (!b.HasValue || !b.Value) { return; }

      string text = vm.Text;

      this.Tasks.Add(new Task(DateTime.Now, text));
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}