using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace TaskTimeTracker {
  internal class MainWindowViewModel : INotifyPropertyChanged {
    private ObservableCollection<Task> _tasks;
    private Task _selectedTask;
    private Visibility _mainWindowVisibility;

    public ObservableCollection<Task> Tasks {
      get { return _tasks; }
      set {
        _tasks = value;
        OnPropertyChanged();
      }
    }

    public Task SelectedTask {
      get { return _selectedTask; }
      set {
        _selectedTask = value;
        OnPropertyChanged();
      }
    }

    public Visibility MainWindowVisibility {
      get { return _mainWindowVisibility; }
      set {
        _mainWindowVisibility = value;
        OnPropertyChanged();
      }
    }

    public ICommand AddCommand { get; set; }
    public ICommand RemoveCommand { get; set; }

    public MainWindowViewModel(IEnumerable<Task> tasks) {
      this.Tasks = new ObservableCollection<Task>(tasks);
      this.AddCommand = new RelayCommand(AddExecute);
      this.RemoveCommand = new RelayCommand(RemoveExecute, o => SelectedTask != null);
      this.MainWindowVisibility = Visibility.Visible;
    }

    private void RemoveExecute(object obj) {
      Tasks.Remove(SelectedTask);
    }

    private void AddExecute(object o) {
      Inbox inbox = new Inbox();
      InboxViewModel vm = new InboxViewModel(inbox);
      inbox.DataContext = vm;
      bool? b = inbox.ShowDialog();

      if (!b.HasValue || !b.Value) { return; }

      string text = vm.Text;

      Tasks.Add(new Task(DateTime.Now, text));
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}