using System;
using System.Reflection;
using System.Windows.Input;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationXmlSerializer<TConfiguration> : IConfigurationXmlSerializer<TConfiguration>
    where TConfiguration : IConfiguration{

    public void Serialize(XmlWriter writer, TConfiguration configuration) {
      ITaskTimeTrackerConfiguration taskTimeTrackerConfiguration = (ITaskTimeTrackerConfiguration) configuration;
      writer.WriteStartDocument();
      writer.WriteStartElement("TaskTimeTrackerConfiguration");
      writer.WriteElementString("Version", Assembly.GetEntryAssembly().GetName().Version.ToString());

      writer.WriteStartElement(nameof(taskTimeTrackerConfiguration.ControlIsChecked));
      writer.WriteElementString(nameof(taskTimeTrackerConfiguration.ControlIsChecked), taskTimeTrackerConfiguration.ControlIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(taskTimeTrackerConfiguration.AltIsChecked));
      writer.WriteElementString(nameof(taskTimeTrackerConfiguration.AltIsChecked), taskTimeTrackerConfiguration.AltIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(taskTimeTrackerConfiguration.WindowsIsChecked));
      writer.WriteElementString(nameof(taskTimeTrackerConfiguration.WindowsIsChecked), taskTimeTrackerConfiguration.WindowsIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(taskTimeTrackerConfiguration.KeyOne));
      writer.WriteElementString(nameof(taskTimeTrackerConfiguration.KeyOne), ((int)taskTimeTrackerConfiguration.KeyOne).ToString());
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndDocument();
    }

    public TConfiguration Deserialize(XmlReader reader) {
      ITaskTimeTrackerConfiguration configuration = new Configuration();
      while (!reader.IsStartElement()) {
        reader.Read();
      }
      reader.ReadStartElement("TaskTimeTrackerConfiguration");
      string versionString = reader.ReadElementString("Version");
      Version version = Version.Parse(versionString);
      
      reader.ReadStartElement(nameof(configuration.ControlIsChecked));
      configuration.ControlIsChecked = reader.ReadElementContentAsBoolean();
      reader.ReadEndElement();

      reader.ReadStartElement(nameof(configuration.AltIsChecked));
      configuration.AltIsChecked = reader.ReadElementContentAsBoolean();
      reader.ReadEndElement();

      reader.ReadStartElement(nameof(configuration.WindowsIsChecked));
      configuration.WindowsIsChecked = reader.ReadElementContentAsBoolean();
      reader.ReadEndElement();

      reader.ReadStartElement(nameof(configuration.KeyOne));
      configuration.KeyOne = (Key)reader.ReadElementContentAsInt();
      reader.ReadEndElement();
      reader.ReadEndElement();

      return (TConfiguration) configuration;
    }
  }
}
