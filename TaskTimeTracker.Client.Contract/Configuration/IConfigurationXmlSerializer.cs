using System.Xml;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfigurationXmlSerializer<TConfiguration>
    where TConfiguration : IConfiguration{
    void Serialize(XmlWriter writer, TConfiguration configuration);
    TConfiguration Deserialize(XmlReader reader);
  }
}