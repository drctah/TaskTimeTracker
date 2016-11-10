using System;
using System.IO;
using System.Text;
using System.Windows.Input;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationController : IConfigurationController {
    private readonly string _configPath;
    public IConfiguration Configuration { get; private set; }
    public IConfigurationXmlSerializer Serializer { get; private set; }

    public ConfigurationController(IConfigurationXmlSerializer serializer) {
      this.Serializer = serializer;
      this._configPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      this._configPath += "/TaskTimeTracker";
      if (!Directory.Exists(this._configPath)) {
        Directory.CreateDirectory(this._configPath);
      }
      this._configPath += @"\TaskTimeTracker.conf";
    }

    public void Save() {
      using (FileStream fileStream = new FileStream(this._configPath, FileMode.OpenOrCreate)) {
        using (XmlTextWriter writer = new XmlTextWriter(fileStream, Encoding.UTF8)) {
          writer.Formatting = Formatting.Indented;
          this.Serializer.Serialize(writer, this.Configuration);
        }
      }
    }

    public IConfiguration Load() {
      IConfiguration result = null;

      if (File.Exists(this._configPath)) {
        using (FileStream fileStream = new FileStream(this._configPath, FileMode.Open)) {
          XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
          xmlReaderSettings.IgnoreWhitespace = true;
          using (XmlReader reader = XmlReader.Create(new XmlTextReader(fileStream), xmlReaderSettings)) {
            result = this.Serializer.Deserialize(reader);
          }
        }
      } else {
        result = new Configuration();
        result.ControlIsChecked = true;
        result.WindowsIsChecked = true;
        result.AltIsChecked = false;
        result.KeyOne = Key.N;
      }

      this.Configuration = result;
      return result;
    }

    public void CreateFromViewModel(IConfigurationWindowViewModel viewModel) {
      this.Configuration = new Configuration();
      this.Configuration.KeyOne = viewModel.KeyOne;
      this.Configuration.AltIsChecked = viewModel.AltIsChecked;
      this.Configuration.ControlIsChecked = viewModel.ControlIsChecked;
      this.Configuration.WindowsIsChecked = viewModel.WindowsIsChecked;
    }
  }
}