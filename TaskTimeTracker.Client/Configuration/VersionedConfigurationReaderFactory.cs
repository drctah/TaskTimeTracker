using System;
using System.Collections.Generic;
using TaskTimeTracker.Client.Contract.Configuration.Serialization;

namespace TaskTimeTracker.Client.Configuration {
  public class VersionedConfigurationReaderFactory {
    readonly Dictionary<Version, IConfigurationVersionedReader> _versionToReaderMapping;

    public VersionedConfigurationReaderFactory() {
      this._versionToReaderMapping = new Dictionary<Version, IConfigurationVersionedReader>();
      AddV1Reader();
    }

    private void AddV1Reader() {
      ConfigurationV1Reader reader = new ConfigurationV1Reader();
      this._versionToReaderMapping.Add(new Version(1, 0, 0, 0), reader);
    }

    public TVersionedReader GetInstance<TVersionedReader>(Version version)
      where TVersionedReader : IConfigurationVersionedReader {
      IConfigurationVersionedReader result = default(TVersionedReader);
      this._versionToReaderMapping.TryGetValue(version, out result);
      return (TVersionedReader)result;
    }
  }
}