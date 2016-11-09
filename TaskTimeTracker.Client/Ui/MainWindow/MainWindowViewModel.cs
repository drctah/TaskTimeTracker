using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using TaskTimeTracker.Client.Contract;
using TaskTimeTracker.Client.Ui.Commands;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;
using TaskTimeTracker.Client.Ui.Inbox;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  internal class MainWindowViewModel : IMainWindowViewModel {
    private ObservableCollection<ITask> _tasks;
    private ITask _selectedTask;
    private Visibility _mainWindowVisibility;
    private ConfigurationWindowViewModel _configViewModel;

    public ObservableCollection<ITask> Tasks {
      get { return this._tasks; }
      set {
        this._tasks = value;
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

    public MainWindowViewModel() {
      this.Tasks = new ObservableCollection<ITask>();
      this.AddCommand = new RelayCommand(AddExecute);
      this.RemoveCommand = new RelayCommand(RemoveExecute);
      this.ConfigCommand = new RelayCommand(ConfigExecute);
      this.MainWindowVisibility = Visibility.Visible;
      this._configViewModel = new ConfigurationWindowViewModel();
    }

    private void ConfigExecute(object obj) {
      ConfigurationWindow.ConfigurationWindow configWindow = new ConfigurationWindow.ConfigurationWindow();
      configWindow.ViewModel = this._configViewModel;
      configWindow.ShowDialog();
    }

    private void RemoveExecute(object obj) {
      if (MessageBox.Show("Sure wanna delete?", "check", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No) != MessageBoxResult.Yes) {
        return;
      }

      this.Tasks.Remove(obj as ITask);
    }

    private void AddExecute(object o) {
      Inbox.Inbox inbox = new Inbox.Inbox();
      InboxViewModel vm = new InboxViewModel(inbox);
      inbox.DataContext = vm;
      bool? b = inbox.ShowDialog();

      if (!b.HasValue || !b.Value) { return; }

      string text = vm.Text;

      DateTime dateTime = DateTime.Now;
      this.Tasks.Add(new Task(dateTime, text));
    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}