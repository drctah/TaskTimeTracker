using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;

namespace TaskTimeTracker.Client.Ui.Inbox {
  /// <summary>
  /// Interaction logic for Inbox.xaml
  /// </summary>
  public partial class Inbox : MetroWindow {
    public Inbox(Window owner) {
      InitializeComponent();

      if (owner != null) {
        this.Owner = owner;
        this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
      }
    }

    private void Inbox_OnLoaded(object sender, RoutedEventArgs e) {
      this.TextBox.Focus();
    }
  }
}
