using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  /// <summary>
  /// Interaction logic for ConfigurationWindow.xaml
  /// </summary>
  public partial class ConfigurationWindow : Window {

    public ConfigurationWindowViewModel ViewModel { get; set; }

    public ConfigurationWindow() {
      InitializeComponent();
    }

    private void OnShortCutInput(object sender, KeyEventArgs e) {
      TextBox senderTextBox = (TextBox)sender;
      string senderName = senderTextBox.Name;

      this.ViewModel.SetKey(senderName, e.Key);
      e.Handled = true;
    }

    private void ConfigurationWindow_OnLoaded(object sender, RoutedEventArgs e) {
      this.DataContext = this.ViewModel;
    }
  }
}
