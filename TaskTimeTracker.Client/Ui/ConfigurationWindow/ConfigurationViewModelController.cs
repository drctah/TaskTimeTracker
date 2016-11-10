using System.Windows;
using System.Windows.Input;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationViewModelController : IConfigurationViewModelController {

    public Window Window { get; }

    public ConfigurationViewModelController(ConfigurationWindow window) {
      this.Window = window;
    }

    /// <summary>
    /// Sets the given key
    /// </summary>
    /// <param name="key"></param>
    /// <param name="viewModel"></param>
    public void SetKey(Key key, IConfigurationWindowViewModel viewModel) {
      viewModel.KeyOne = key;
      viewModel.KeyOneString = key.ToString();
    }

    public void ExecuteCancel(object obj) {
      this.Window.Close();
    }

    public void ExecuteOk(object obj) {
      ((ConfigurationWindow)this.Window).ConfigurationController.CreateFromViewModel(((ConfigurationWindow)this.Window).ViewModel);
      this.Window.Close();
    }

    public IConfigurationWindowViewModel FromConfiguration(IConfiguration configuration) {
      IConfigurationWindowViewModel result = new ConfigurationWindowViewModel(this);
      result.AltIsChecked = configuration.AltIsChecked;
      result.ControlIsChecked = configuration.ControlIsChecked;
      result.WindowsIsChecked = configuration.WindowsIsChecked;
      SetKey(configuration.KeyOne, result);
      return result;
    }
  }
}