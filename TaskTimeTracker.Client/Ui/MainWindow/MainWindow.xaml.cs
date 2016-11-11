using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using MahApps.Metro.Controls;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow {
    private MainWindowViewModel _viewModel;

    public MainWindow() {
      InitializeComponent();
      IConfigurationController controller = new ConfigurationController(new ConfigurationXmlSerializer<ITaskTimeTrackerConfiguration>());
      controller.Load();
      this._viewModel = new MainWindowViewModel(controller);
      this.DataContext = this._viewModel;
    }

    private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
      e.Cancel = true;
      this.Visibility = Visibility.Hidden;
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
      this._viewModel.OnWindowLoaded();
    }
  }
}
