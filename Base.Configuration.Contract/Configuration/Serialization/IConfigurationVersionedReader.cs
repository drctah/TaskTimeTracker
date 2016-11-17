using System.Xml;

namespace Base.Configuration.Contract.Configuration.Serialization {
  /// <summary>
  /// Wrapperinterface for VersionedConfigurationReader
  /// </summary>
  public interface IConfigurationVersionedReader : IVersionedReader<ITaskTimeTrackerConfiguration, XmlReader> {
  }
}