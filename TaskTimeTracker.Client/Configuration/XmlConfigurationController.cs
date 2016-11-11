using System.Xml;
using Base.Serialization.Contract;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Configuration {
  public abstract class XmlConfigurationController<TSerializer, TViewModel> :
    ConfigurationController<TSerializer, XmlWriter, XmlReader, TViewModel>
    where TSerializer : ISerializer<IConfiguration, XmlWriter, XmlReader> {

    protected XmlConfigurationController(TSerializer serializer) : base(serializer) {
    }
  }
}