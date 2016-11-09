using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using TaskTimeTracker.Client.Ui.Commands;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;
using TaskTimeTracker.Client.Ui.Inbox;
using TaskTimeTracker.Configuration;
using TaskTimeTracker.Configuration.Contract;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  internal class MainWindowViewModel {
    private ObservableCollection<Task> _tasks;
    private Visibility _mainWindowVisibility;
    private IConfigurationWindowViewModel _configViewModel;
    private readonly TaskTimeTrackerConfigurationController _configurationController;

    public Task SelectedTask { get; set; }

    public ObservableCollection<Task> Tasks {
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

    /// <summary>
    /// Dem TaskTimeTrackerConfiguration
    /// </summary>
    public ITaskTimeTrackerConfiguration Configuration { get; set; }

    public ICommand MouseDoubleClick { get; set; }

    public MainWindowViewModel(TaskTimeTrackerConfigurationController configurationController) {
      this.Tasks = new ObservableCollection<Task>();
      this.AddCommand = new RelayCommand(AddExecute);
      this.RemoveCommand = new RelayCommand(RemoveExecute);
      this.ConfigCommand = new RelayCommand(ConfigExecute);
      this.MouseDoubleClick = new RelayCommand(this.MouseDoubleClickExecute);
      this.MainWindowVisibility = Visibility.Visible;
      this._configurationController = configurationController;
      this.Configuration = (ITaskTimeTrackerConfiguration)this._configurationController.Configuration;
      Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(OnSystemSessenSwitchEvent);
    }

    private void OnSystemSessenSwitchEvent(object sender, SessionSwitchEventArgs e) {
      if (!this.Configuration.SetStampOnLockIsChecked) {
        return;
      }

      if (e.Reason == SessionSwitchReason.SessionLock) {
        this.AddNewTimeStamp(this.Configuration.ScreenLockedText);
      } else if (e.Reason == SessionSwitchReason.SessionUnlock) {
        AddNewTimeStamp(this.Configuration.ScreenUnlockedText);
      }
    }

    private void MouseDoubleClickExecute(object o) {
      if (this.SelectedTask == null || this.SelectedTask.EditMode) {
        return;
      }

      this.SelectedTask.EnterEditMode();
    }

    private void ConfigExecute(object obj) {
      ConfigurationWindow.ConfigurationWindow configWindow = new ConfigurationWindow.ConfigurationWindow(Application.Current.MainWindow);
      configWindow.ConfigurationController = this._configurationController;
      ConfigurationViewModelController configurationViewModelController = new ConfigurationViewModelController(configWindow);
      this._configViewModel = configurationViewModelController.FromConfiguration(this.Configuration);
      configWindow.ViewModel = this._configViewModel;
      configWindow.ShowDialog();

      if (this.Configuration.CompareTo((ITaskTimeTrackerConfiguration)this._configurationController.Configuration) == 0) {
        return;
      }

      this._configurationController.Save();
      this.Configuration = (ITaskTimeTrackerConfiguration)this._configurationController.Configuration;
    }

    private void RemoveExecute(object obj) {
      if (MessageBox.Show("Sure wanna delete?", "check", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.No) != MessageBoxResult.Yes) {
        return;
      }

      this.Tasks.Remove(obj as Task);
    }

    private void AddExecute(object o) {
      Inbox.Inbox inbox = new Inbox.Inbox(Application.Current.MainWindow);
      InboxViewModel inboxViewModel = new InboxViewModel(inbox);
      inbox.DataContext = inboxViewModel;
      bool? dialogResult = inbox.ShowDialog();

      if (!dialogResult.HasValue || !dialogResult.Value) { return; }

      string text = inboxViewModel.Text;

      AddNewTimeStamp(text);
    }

    private void AddNewTimeStamp(string text) {
      DateTime dateTime = DateTime.Now;
      Task newTask = new Task(dateTime, text);
      this.Tasks.Add(newTask);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
      this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public void OnWindowLoaded() {
      if (!this.Configuration.SetStampOnStartupIsChecked) {
        return;
      }

      this.Tasks.Add(new Task(DateTime.Now, this.Configuration.StartupStampText));
    }
  }
}
