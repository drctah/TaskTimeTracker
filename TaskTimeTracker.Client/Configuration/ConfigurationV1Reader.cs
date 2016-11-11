using System;
using System.Windows.Input;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;
using TaskTimeTracker.Client.Contract.Configuration.Serialization;

namespace TaskTimeTracker.Client.Configuration {
  public class ConfigurationV1Reader : IConfigurationVersionedReader {
    public Version MatchVersion { get { return new Version(1, 0); } }

    public ITaskTimeTrackerConfiguration Read(XmlReader reader) {
      ITaskTimeTrackerConfiguration result = new TaskTimeTrackerConfiguration();
      ReadShortCutSection(result, reader);
      ReadSetStampOnStartupSection(result, reader);
      return result;
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
