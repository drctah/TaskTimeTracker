using System.Windows;
using System.Windows.Input;
using MahApps.Metro.Controls;
using TaskTimeTracker.Client.Configuration;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  /// <summary>
  /// Interaction logic for ConfigurationWindow.xaml
  /// </summary>
  public partial class ConfigurationWindow : MetroWindow {

    public IConfigurationWindowViewModel ViewModel { get; set; }
    public TaskTimeTrackerConfigurationController ConfigurationController { get; set; }

    public ConfigurationWindow(Window owner) {
      InitializeComponent();

      if (owner != null) {
        this.Owner = owner;
        this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
      }
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
