using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  /// <summary>
  /// Interaction logic for ConfigurationWindow.xaml
  /// </summary>
  public partial class ConfigurationWindow : MetroWindow {

    public IConfigurationWindowViewModel ViewModel { get; set; }
    public IConfigurationController ConfigurationController { get; set; }

    public ConfigurationWindow() {
      InitializeComponent();
    }

    private void OnShortCutInput(object sender, KeyEventArgs e) {
      this.ViewModel.Controller.SetKey(e.Key, this.ViewModel);
      e.Handled = true;
    }

    private void ConfigurationWindow_OnLoaded(object sender, RoutedEventArgs e) {
      this.DataContext = this.ViewModel;
    }
  }
}
