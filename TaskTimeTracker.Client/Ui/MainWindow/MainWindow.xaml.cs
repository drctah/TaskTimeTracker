using System;
using System.ComponentModel;
using System.Threading;
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
      SetLanguageDictionary();
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

    private void SetLanguageDictionary() {
      ResourceDictionary dict = new ResourceDictionary();
      switch (Thread.CurrentThread.CurrentCulture.ToString()) {
        //case "de-DE":
        //  dict.Source = new Uri("../Multilingual/German.xaml", UriKind.Relative);
        //  break;
        default:
          dict.Source = new Uri("../Multilingual/English.xaml", UriKind.Relative);
          break;
      }
      this.Resources.MergedDictionaries.Add(dict);
    }
  }
}
