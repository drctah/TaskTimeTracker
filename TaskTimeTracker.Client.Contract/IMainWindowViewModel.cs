using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Contract {
  public interface IMainWindowViewModel : INotifyPropertyChanged {
    /// <summary>
    /// Collection used for Tasks
    /// </summary>
    ObservableCollection<ITask> Tasks { get; set; }

    ///// <summary>
    ///// The selected Task
    ///// </summary>
    //ITask SelectedTask { get; set; }

    /// <summary>
    /// Visibility of the MainWindow
    /// </summary>
    Visibility MainWindowVisibility { get; set; }

    /// <summary>
    /// The AddCommand
    /// </summary>
    ICommand AddCommand { get; set; }

    /// <summary>
    /// The RemoveCommand
    /// </summary>
    ICommand RemoveCommand { get; set; }
  }
}