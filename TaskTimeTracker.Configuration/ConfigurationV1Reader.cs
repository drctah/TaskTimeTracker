using System;
using System.Windows.Input;
using System.Xml;
using Base.Configuration.Contract.Configuration;
using Base.Configuration.Contract.Configuration.Serialization;
using TaskTimeTracker.Configuration.Contract;

namespace TaskTimeTracker.Configuration {
  public class ConfigurationV1Reader : IConfigurationVersionedReader {
    public Version MatchVersion { get { return new Version(1, 0, 0, 0); } }

    public IConfiguration Read(XmlReader reader) {
      ITaskTimeTrackerConfiguration result = new TaskTimeTrackerConfiguration();
      ReadShortCutSection(result, reader);
      ReadSetStampOnStartupSection(result, reader);
      ReadSetStampOnLockScreen(result, reader);
      return result;
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

    private void ReadSetStampOnLockScreen(ITaskTimeTrackerConfiguration configuration, XmlReader reader) {
      if (String.Compare(reader.Name, nameof(configuration.SetStampOnLockIsChecked), StringComparison.Ordinal) != 0) {
        return;
      }

      reader.ReadStartElement(nameof(configuration.SetStampOnLockIsChecked));
      configuration.SetStampOnLockIsChecked = reader.ReadElementContentAsBoolean();
      reader.ReadEndElement();

      reader.ReadStartElement(nameof(configuration.ScreenLockedText));
      configuration.ScreenLockedText = reader.ReadElementString(nameof(configuration.ScreenLockedText));
      reader.ReadEndElement();

      reader.ReadStartElement(nameof(configuration.ScreenUnlockedText));
      configuration.ScreenUnlockedText = reader.ReadElementString(nameof(configuration.ScreenUnlockedText));
      reader.ReadEndElement();
    }
  }
}
