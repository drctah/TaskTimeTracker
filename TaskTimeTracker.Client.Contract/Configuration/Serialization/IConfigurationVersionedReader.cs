using System.Xml;
using TaskTimeTracker.IO.Contract;

namespace TaskTimeTracker.Client.Contract.Configuration.Serialization {
  /// <summary>
  /// Wrapperinterface for VersionedConfigurationReader
  /// </summary>
  public interface IConfigurationVersionedReader : IVersionedReader<ITaskTimeTrackerConfiguration, XmlReader> {
  }
}