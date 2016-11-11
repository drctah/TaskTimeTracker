using System;
using System.Reflection;
using System.Windows.Input;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationXmlSerializer<TConfiguration> : IConfigurationXmlSerializer<TConfiguration>
    where TConfiguration : IConfiguration {

    public void Serialize(XmlWriter writer, TConfiguration configuration) {
      ITaskTimeTrackerConfiguration taskTimeTrackerConfiguration = (ITaskTimeTrackerConfiguration)configuration;
      writer.WriteStartDocument();
      writer.WriteStartElement("TaskTimeTrackerConfiguration");
      WriteVersion(writer);
      WriteShortCutSection(writer, taskTimeTrackerConfiguration);
      WriteSetStampOnStartupSection(writer, taskTimeTrackerConfiguration);
      writer.WriteEndElement();
      writer.WriteEndDocument();
    }

    private void WriteVersion(XmlWriter writer) {
      writer.WriteElementString("Version", Assembly.GetEntryAssembly().GetName().Version.ToString());
    }

    private void WriteShortCutSection(XmlWriter writer, ITaskTimeTrackerConfiguration configuration) {
      writer.WriteStartElement(nameof(configuration.ControlIsChecked));
      writer.WriteElementString(nameof(configuration.ControlIsChecked), configuration.ControlIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(configuration.AltIsChecked));
      writer.WriteElementString(nameof(configuration.AltIsChecked), configuration.AltIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(configuration.WindowsIsChecked));
      writer.WriteElementString(nameof(configuration.WindowsIsChecked), configuration.WindowsIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(configuration.KeyOne));
      writer.WriteElementString(nameof(configuration.KeyOne), ((int)configuration.KeyOne).ToString());
      writer.WriteEndElement();
    }

    private void WriteSetStampOnStartupSection(XmlWriter writer, ITaskTimeTrackerConfiguration configuration) {
      writer.WriteStartElement(nameof(configuration.SetStampOnStartupIsChecked));
      writer.WriteElementString(nameof(configuration.SetStampOnStartupIsChecked), configuration.SetStampOnStartupIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(configuration.StartupStampText));
      writer.WriteElementString(nameof(configuration.StartupStampText), configuration.StartupStampText);
      writer.WriteEndElement();
    }

    public TConfiguration Deserialize(XmlReader reader) {
      ITaskTimeTrackerConfiguration configuration = new Configuration();
      while (!reader.IsStartElement()) {
        reader.Read();
      }
      reader.ReadStartElement("TaskTimeTrackerConfiguration");
      ReadVersion(reader);
      ReadShortCutSection(configuration, reader);
      ReadSetStampOnStartupSection(configuration, reader);
      reader.ReadEndElement();

      return (TConfiguration)configuration;
    }

    private void ReadSetStampOnStartupSection(ITaskTimeTrackerConfiguration configuration, XmlReader reader) {
      if (String.Compare(reader.Name, nameof(configuration.SetStampOnStartupIsChecked), StringComparison.Ordinal) != 0) {
        return;
      }

      reader.ReadStartElement(nameof(configuration.SetStampOnStartupIsChecked));
      configuration.SetStampOnStartupIsChecked = reader.ReadElementContentAsBoolean();
      reader.ReadEndElement();

      reader.ReadStartElement(nameof(configuration.StartupStampText));
      configuration.StartupStampText = reader.ReadElementString(nameof(configuration.StartupStampText));
      reader.ReadEndElement();
    }

    private void ReadVersion(XmlReader reader) {
      string versionString = reader.ReadElementString("Version");
      Version version = Version.Parse(versionString);
    }

    private void ReadShortCutSection(ITaskTimeTrackerConfiguration configuration, XmlReader reader) {
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
    }
  }
}
