using System.IO;
using System.Text;
using System.Windows.Input;
using System.Xml;
using Base.Configuration;
using Base.Configuration.Contract.Configuration;
using TaskTimeTracker.Configuration.Contract;

namespace TaskTimeTracker.Configuration {
  public class TaskTimeTrackerConfigurationController : XmlConfigurationController<XmlConfigurationSerializer<IConfiguration>, IConfigurationWindowViewModel> {

    public TaskTimeTrackerConfigurationController(XmlConfigurationSerializer<IConfiguration> serializer, string configPath)
      : base(serializer, configPath) {
    }

    public override void Save() {
      using (FileStream fileStream = new FileStream(this.ConfigPath, FileMode.OpenOrCreate)) {
        using (XmlTextWriter writer = new XmlTextWriter(fileStream, Encoding.UTF8)) {
          writer.Formatting = Formatting.Indented;
          this.Serializer.Serialize(writer, this.Configuration);
        }
      }
    }

    public override IConfiguration Load() {
      ITaskTimeTrackerConfiguration result = null;

      if (File.Exists(this.ConfigPath)) {
        using (FileStream fileStream = new FileStream(this.ConfigPath, FileMode.Open)) {
          XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
          xmlReaderSettings.IgnoreWhitespace = true;
          using (XmlReader reader = XmlReader.Create(new XmlTextReader(fileStream), xmlReaderSettings)) {
            result = (ITaskTimeTrackerConfiguration)this.Serializer.Deserialize(reader);
          }
        }
      } else {
        result = (ITaskTimeTrackerConfiguration)CreateDefaultConfiguration();
      }

      this.Configuration = result;
      return result;
    }

    public override IConfiguration CreateDefaultConfiguration() {
      TaskTimeTrackerConfiguration result = new TaskTimeTrackerConfiguration();
      result.ControlIsChecked = true;
      result.WindowsIsChecked = true;
      result.AltIsChecked = false;
      result.KeyOne = Key.N;

      result.ScreenLockedText = "Away";
      result.ScreenUnlockedText = "Back";
      return result;
    }


    public override void CreateFromViewModel(IConfigurationWindowViewModel viewModel) {
      ITaskTimeTrackerConfiguration config = new TaskTimeTrackerConfiguration();
      config.KeyOne = viewModel.KeyOne;
      config.AltIsChecked = viewModel.AltIsChecked;
      config.ControlIsChecked = viewModel.ControlIsChecked;
      config.WindowsIsChecked = viewModel.WindowsIsChecked;

      config.StartupStampText = viewModel.StartupStampText;
      config.SetStampOnStartupIsChecked = viewModel.SetStampOnStartupIsChecked;

      config.SetStampOnLockIsChecked = viewModel.SetStampOnLockIsChecked;
      config.ScreenLockedText = viewModel.ScreenLockedText;
      config.ScreenUnlockedText = viewModel.ScreenUnlockedText;

      this.Configuration = config;
    }
  }
}