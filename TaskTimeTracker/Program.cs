using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace TaskTimeTracker {
  public class Program : IDisposable {
    private NotifyIcon _notifyIcon;
    private Stream _iconStream;
    private bool _closed;
    private MainWindow mainWindow;
    private ICollection<Task> _tasks;

    [STAThread]
    static void Main(string[] args) {
      using (Program p = new Program()) {
        p.Run();
      }
    }

    public Program() {
      this._iconStream = GetType().Assembly.GetManifestResourceStream("TaskTimeTracker.Clock.ico");

      if (_iconStream == null) { throw new InvalidOperationException(); }

      this.Shutdown = false;

      this._notifyIcon = new NotifyIcon();

      MenuItem menuItem = new MenuItem();
      menuItem.Click += Close;
      menuItem.Text = "Close";

      MenuItem[] menuItems = { menuItem };

      this._notifyIcon.ContextMenu = new ContextMenu(menuItems);
      this._notifyIcon.Icon = new System.Drawing.Icon(_iconStream);
      this._notifyIcon.DoubleClick += NotifyIconOnDoubleClick;

      this._closed = true;

      this._tasks = new List<Task>();
    }

    private void Close(object sender, EventArgs eventArgs) {
      if (!this._closed) {
        this.mainWindow.Dispatcher.Invoke(this.mainWindow.Close);
      }

      this._notifyIcon.Visible = true;
      this.Shutdown = true;
    }

    public bool Shutdown { get; private set; }

    private void NotifyIconOnDoubleClick(object sender, EventArgs eventArgs) {
      OpenWindow();
    }

    private void OpenWindow() {
      if (!this._closed) {
        return;
      }
      this._closed = false;

      Thread t = new Thread(OpenWindowRun);
      t.SetApartmentState(ApartmentState.STA);
      t.Start();
    }

    private void OpenWindowRun() {
      this.mainWindow = new MainWindow();
      mainWindow.DataContext = new MainWindowViewModel(this._tasks);
      mainWindow.Closed += MainWindowOnClosed;
      mainWindow.ShowDialog();
    }

    private void MainWindowOnClosed(object sender, EventArgs eventArgs) {
      MainWindow window = sender as MainWindow;
      MainWindowViewModel mainWindowViewModel = window.DataContext as MainWindowViewModel;
      this._tasks.Clear();
      foreach (Task task in mainWindowViewModel.Tasks) {
        this._tasks.Add(task);
      }

      this._closed = true;
      this.mainWindow = null;
    }

    private void Run() {
      OpenWindow();
      this._notifyIcon.Visible = true;

      while (!Shutdown) {
        System.Threading.Thread.CurrentThread.Join(100);
      }
    }

    public void Dispose() {
      _iconStream?.Dispose();
    }
  }
}
