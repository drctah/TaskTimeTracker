using Base.Configuration.Contract.Configuration;
using Base.Serialization.Contract;

namespace Base.Configuration {
  public abstract class ConfigurationController<TSerializer, TSerializationWriter, TSerializationReader, TViewModel> :
    IConfigurationController<IConfiguration, TViewModel, TSerializer>
    where TSerializer : ISerializer<IConfiguration, TSerializationWriter, TSerializationReader> {
    protected readonly string ConfigPath;
    public IConfiguration Configuration { get; protected set; }
    public TSerializer Serializer { get; }

    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="serializer"></param>
    /// <param name="configPath"></param>
    protected ConfigurationController(TSerializer serializer, string configPath) {
      this.Serializer = serializer;
      this.ConfigPath = configPath;
    }
    
    public abstract void Save();

    public abstract IConfiguration Load();

    public abstract IConfiguration CreateDefaultConfiguration();

    /// <summary>
    /// Creates the config from a viewModel
    /// </summary>
    /// <param name="viewModel"></param>
    public abstract void CreateFromViewModel(TViewModel viewModel);
  }
}
