using System.Windows;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Ui.Inbox {
  /// <summary>
  /// Interaction logic for Inbox.xaml
  /// </summary>
  public partial class Inbox : Window {
    public Inbox() {
      InitializeComponent();
    }

    private void Inbox_OnLoaded(object sender, RoutedEventArgs e) {
      this.TextBox.Focus();
    }


    private void OnKeyUp(object sender, KeyEventArgs e) {
      if (e.Key == Key.Enter) {
        this.DialogResult = true;
        Close();
      }
    }
  }
}
