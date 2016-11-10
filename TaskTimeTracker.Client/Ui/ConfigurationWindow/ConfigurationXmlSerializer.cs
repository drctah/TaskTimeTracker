using System.Xml;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Ui.ConfigurationWindow {
  public class ConfigurationXmlSerializer : IConfigurationXmlSerializer {

    public void Serialize(XmlWriter writer, IConfiguration configuration) {
      throw new System.NotImplementedException();
    }

    public IConfiguration Deserialize(XmlReader reader) {
      throw new System.NotImplementedException();
    }
  }
}
