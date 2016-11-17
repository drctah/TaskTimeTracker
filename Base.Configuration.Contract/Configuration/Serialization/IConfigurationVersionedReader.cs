using System.Xml;
using TaskTimeTracker.IO.Contract;

namespace Base.Configuration.Contract.Configuration.Serialization {
  /// <summary>
  /// Wrapperinterface for VersionedConfigurationReader
  /// </summary>
  public interface IConfigurationVersionedReader : IVersionedReader<IConfiguration, XmlReader> {
  }
}