using System.Xml;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfigurationXmlSerializer {
    void Serialize(XmlWriter writer, IConfiguration configuration);
    IConfiguration Deserialize(XmlReader reader);
  }
}