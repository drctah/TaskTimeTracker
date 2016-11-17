using System;
using System.Xml;
using Base.Configuration.Contract.Configuration;

namespace Base.Configuration {
  public abstract class XmlConfigurationSerializer<TConfiguration> : ConfigurationSerializer<TConfiguration, XmlWriter, XmlReader>
    where TConfiguration : IConfiguration {
    /// <summary>
    /// Serialize
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="configuration"></param>
    public override void Serialize(XmlWriter writer, TConfiguration configuration) {
      writer.WriteStartDocument();
      writer.WriteStartElement("TaskTimeTrackerConfiguration");
      WriteVersion(writer, configuration.Version);
      SerializeInternal(writer, configuration);
      writer.WriteEndElement();
      writer.WriteEndDocument();
    }

    /// <summary>
    /// Writes the version
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="version"></param>
    protected override void WriteVersion(XmlWriter writer, Version version) {
      writer.WriteElementString("Version", version.ToString());
    }

    /// <summary>
    /// Deserialize
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public override TConfiguration Deserialize(XmlReader reader) {
      while (!reader.IsStartElement()) {
        reader.Read();
      }

      reader.ReadStartElement("TaskTimeTrackerConfiguration");
      Version version = ReadVersion(reader);
      TConfiguration result = DeserializeInternal(reader, version);
      reader.ReadEndElement();

      return result;
    }

    protected abstract override TConfiguration DeserializeInternal(XmlReader reader, Version version);

    /// <summary>
    /// Reads the Configurationversion
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    protected override Version ReadVersion(XmlReader reader) {
      string versionString = reader.ReadElementString("Version");
      Version version = Version.Parse(versionString);
      return version;
    }
  }
}