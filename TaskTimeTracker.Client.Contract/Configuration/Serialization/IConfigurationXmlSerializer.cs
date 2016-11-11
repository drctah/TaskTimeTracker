using System.Windows.Markup;
using System.Xml;

namespace TaskTimeTracker.Client.Contract.Configuration.Serialization {
  public interface IConfigurationXmlSerializer<TConfiguration> : ISerializer<TConfiguration, XmlWriter, XmlReader>
    where TConfiguration : IConfiguration {
    void Serialize(XmlWriter writer, TConfiguration configuration);
    TConfiguration Deserialize(XmlReader reader);
  }
}