using System.ComponentModel;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Contract {
  public interface IInboxViewModel : INotifyPropertyChanged {
    /// <summary>
    /// Command used for the OkButton
    /// </summary>
    ICommand OkCommand { get; set; }
    /// <summary>
    /// Command used for the AbortButton
    /// </summary>
    ICommand AbortCommand { get; set; }
    /// <summary>
    /// The inserted Text
    /// </summary>
    string Text { get; set; }
  }
}