using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Base.Configuration.Contract.Configuration;
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

    public abstract IConfiguration CreateDefaultConfiguration();

    /// <summary>
    /// Creates the config from a viewModel
    /// </summary>
    /// <param name="viewModel"></param>
    public abstract void CreateFromViewModel(TViewModel viewModel);
  }
}
