using System;
using System.IO;
using Base.Serialization.Contract;
using TaskTimeTracker.Client.Contract.Configuration;

namespace TaskTimeTracker.Client.Configuration {
  public abstract class ConfigurationController<TSerializer, TSerializationWriter, TSerializationReader, TViewModel> :
    IConfigurationController<IConfiguration, TViewModel, TSerializer>
    where TSerializer : ISerializer<IConfiguration, TSerializationWriter, TSerializationReader> {
    protected readonly string ConfigPath;
    public ITaskTimeTrackerConfiguration Configuration { get; protected set; }
    IConfiguration IConfigurationController<IConfiguration, TViewModel, TSerializer>.Configuration { get; }
    public TSerializer Serializer { get; }

    protected ConfigurationController(TSerializer serializer) {
      this.Serializer = serializer;
      this.ConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      this.ConfigPath += "/TaskTimeTracker";
      if (!Directory.Exists(this.ConfigPath)) {
        Directory.CreateDirectory(this.ConfigPath);
      }
      this.ConfigPath += @"\TaskTimeTracker.conf";
    }

    public abstract void Save();

    public abstract IConfiguration Load();

    public abstract void CreateFromViewModel(TViewModel viewModel);
  }
}