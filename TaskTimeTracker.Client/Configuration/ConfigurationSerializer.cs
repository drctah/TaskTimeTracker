using System;
using Base.Configuration.Contract.Configuration;
using Base.Serialization.Contract;

namespace TaskTimeTracker.Client.Configuration {
  public abstract class ConfigurationSerializer<TConfiguration, TSerializer, TDeserializer> : ISerializer<TConfiguration, TSerializer, TDeserializer>
    where TConfiguration : IConfiguration {

    public virtual void Serialize(TSerializer writer, TConfiguration configuration) {
      WriteVersion(writer, configuration.Version);
      SerializeInternal(writer, configuration);
    }

    /// <summary>
    /// Writes the version
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="version"></param>
    protected abstract void WriteVersion(TSerializer writer, Version version);

    protected abstract void SerializeInternal(TSerializer writer, TConfiguration configuration);

    /// <summary>
    /// Deserialize
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    public virtual TConfiguration Deserialize(TDeserializer reader) {
      Version version = ReadVersion(reader);
      return DeserializeInternal(reader, version);
    }

    protected abstract TConfiguration DeserializeInternal(TDeserializer reader, Version version);

    /// <summary>
    /// Reads the Configurationversion
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    protected abstract Version ReadVersion(TDeserializer reader);
  }
}
