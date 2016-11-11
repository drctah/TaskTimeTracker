namespace TaskTimeTracker.Client.Contract.Configuration {
  /// <summary>
  /// ConfigurationController
  /// </summary>
  /// <typeparam name="TConfiguration"></typeparam>
  /// <typeparam name="TViewModel"></typeparam>
  /// <typeparam name="TSerializer"></typeparam>
  public interface IConfigurationController<TConfiguration, TViewModel, TSerializer>
    where TConfiguration : IConfiguration {
    /// <summary>
    /// The Configuration
    /// </summary>
    TConfiguration Configuration { get; }

    /// <summary>
    /// Used for serialization
    /// </summary>
    TSerializer Serializer { get; }
    /// <summary>
    /// Saves the config
    /// </summary>
    void Save();
    /// <summary>
    /// Loads the config
    /// </summary>
    /// <returns></returns>
    TConfiguration Load();

    /// <summary>
    /// Creates the config from a viewModel
    /// </summary>
    /// <param name="viewModel"></param>
    void CreateFromViewModel(TViewModel viewModel);
  }
}