using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace TaskTimeTracker.Client.Ui.Inbox {
  /// <summary>
  /// Interaction logic for Inbox.xaml
  /// </summary>
  public partial class Inbox : MetroWindow {
    public Inbox(Window mainWindow) {
      InitializeComponent();
      this.Owner = mainWindow;
    }

    private void Inbox_OnLoaded(object sender, RoutedEventArgs e) {
      this.TextBox.Focus();
    }


    private void OnKeyUp(object sender, KeyEventArgs e) {
      if (e.Key == Key.Enter) {
        this.DialogResult = true;
        Close();
      } else if (e.Key == Key.Escape) {
        Close();
      }
    }
  }
}
