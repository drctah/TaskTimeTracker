using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Ui.ConfigurationWindow;

namespace TaskTimeTracker.Client.Ui.MainWindow {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
      IConfigurationController controller = new ConfigurationController(new ConfigurationXmlSerializer());
      controller.Load();
      this.DataContext = new MainWindowViewModel(controller);
    }

    private void MainWindow_OnClosing(object sender, CancelEventArgs e) {
      e.Cancel = true;
      this.Visibility = Visibility.Hidden;
    }
  }
}
