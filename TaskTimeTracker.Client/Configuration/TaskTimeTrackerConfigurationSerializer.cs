using System;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Contract.Configuration.Serialization;

namespace TaskTimeTracker.Client.Configuration {
  public class TaskTimeTrackerConfigurationSerializer : XmlConfigurationSerializer<IConfiguration> {
    protected override void SerializeInternal(XmlWriter writer, IConfiguration configuration) {
      WriteShortCutSection(writer, configuration);
      WriteSetStampOnStartupSection(writer, configuration);
    }

    protected override IConfiguration DeserializeInternal(XmlReader reader, Version version) {
      VersionedConfigurationReaderFactory factory = new VersionedConfigurationReaderFactory();
      IConfigurationVersionedReader configurationV1Reader = factory.GetInstance<ConfigurationV1Reader>(version);
      ITaskTimeTrackerConfiguration result = configurationV1Reader.Read(reader);
      return result;
    }

    private void WriteShortCutSection(XmlWriter writer, IConfiguration configuration) {
      ITaskTimeTrackerConfiguration taskTimeTrackerConfiguration = (ITaskTimeTrackerConfiguration)configuration;
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
    }

    private void WriteSetStampOnStartupSection(XmlWriter writer, IConfiguration configuration) {
      ITaskTimeTrackerConfiguration taskTimeTrackerConfiguration = (ITaskTimeTrackerConfiguration)configuration;
      writer.WriteStartElement(nameof(taskTimeTrackerConfiguration.SetStampOnStartupIsChecked));
      writer.WriteElementString(nameof(taskTimeTrackerConfiguration.SetStampOnStartupIsChecked), taskTimeTrackerConfiguration.SetStampOnStartupIsChecked ? "1" : "0");
      writer.WriteEndElement();

      writer.WriteStartElement(nameof(taskTimeTrackerConfiguration.StartupStampText));
      writer.WriteElementString(nameof(taskTimeTrackerConfiguration.StartupStampText), taskTimeTrackerConfiguration.StartupStampText);
      writer.WriteEndElement();
    }
  }
}