using System;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Application = System.Windows.Application;

namespace TaskTimeTracker.Client {
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application {
    private readonly NotifyIcon _notifyIcon;

    public App() {
      MenuItem menuItem = new MenuItem();
      var iconStream = GetType().Assembly.GetManifestResourceStream("TaskTimeTracker.Client.Clock.ico");

      if (iconStream == null) { throw new InvalidOperationException(); }

      menuItem.Click += Close;
      menuItem.Text = "Close";

      MenuItem[] menuItems = { menuItem };

      this._notifyIcon = new NotifyIcon();
      this._notifyIcon.ContextMenu = new ContextMenu(menuItems);
      this._notifyIcon.Icon = new System.Drawing.Icon(iconStream);
      this._notifyIcon.DoubleClick += NotifyIconOnDoubleClick;
      this._notifyIcon.Visible = true;
    }

    private void NotifyIconOnDoubleClick(object sender, EventArgs e) {
      this.MainWindow.Visibility = Visibility.Visible;
    }

    private void Close(object sender, EventArgs e) {
      this._notifyIcon.Visible = false;
      Environment.Exit(0);
    }
  }
}
