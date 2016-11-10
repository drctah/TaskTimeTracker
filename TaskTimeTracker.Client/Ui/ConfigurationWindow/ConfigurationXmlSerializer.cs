using System;
using System.Reflection;
using System.Windows.Input;
using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationXmlSerializer : IConfigurationXmlSerializer {

    public void Serialize(XmlWriter writer, IConfiguration configuration) {
      writer.WriteStartDocument();
      writer.WriteStartElement("TaskTimeTrackerConfiguration");
      writer.WriteElementString("Version", Assembly.GetEntryAssembly().GetName().Version.ToString());

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
      writer.WriteEndElement();
      writer.WriteEndDocument();
    }

    public IConfiguration Deserialize(XmlReader reader) {
      IConfiguration configuration = new Configuration();
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

      return configuration;
    }
  }
}
