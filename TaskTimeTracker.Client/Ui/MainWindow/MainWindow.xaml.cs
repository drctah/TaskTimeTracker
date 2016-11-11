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
    public MainWindow() {
      InitializeComponent();
      IConfigurationController controller = new ConfigurationController(new ConfigurationXmlSerializer<ITaskTimeTrackerConfiguration>());
      controller.Load();
      this.DataContext = new MainWindowViewModel(controller);
    }

    private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
      e.Cancel = true;
      this.Visibility = Visibility.Hidden;
    }
  }
}
