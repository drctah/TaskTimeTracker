using System;
using System.ComponentModel;
using System.IO;
using System.Windows;

using MahApps.Metro.Controls;
using TaskTimeTracker.Configuration;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : MetroWindow {
    private MainWindowViewModel _viewModel;

    public MainWindow() {
      InitializeComponent();
      TaskTimeTrackerConfigurationSerializer serializer = new TaskTimeTrackerConfigurationSerializer();
      string configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      configPath += "/TaskTimeTracker";
      if (!Directory.Exists(configPath)) {
        Directory.CreateDirectory(configPath);
      }
      configPath += @"\TaskTimeTracker.conf";
      TaskTimeTrackerConfigurationController controller = new TaskTimeTrackerConfigurationController(serializer, configPath);
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
