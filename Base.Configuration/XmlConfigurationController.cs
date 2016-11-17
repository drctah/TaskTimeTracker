using System.Xml;
using Base.Configuration.Contract.Configuration;
using Base.Serialization.Contract;

namespace Base.Configuration {
  public abstract class XmlConfigurationController<TSerializer, TViewModel> :
    ConfigurationController<TSerializer, XmlWriter, XmlReader, TViewModel>
    where TSerializer : ISerializer<IConfiguration, XmlWriter, XmlReader> {

    protected XmlConfigurationController(TSerializer serializer, string configPath) : base(serializer, configPath) {
    }
  }
}