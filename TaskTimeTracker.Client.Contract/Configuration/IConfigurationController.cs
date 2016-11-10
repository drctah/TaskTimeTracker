namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfigurationController {
    /// <summary>
    /// The Configuration
    /// </summary>
    IConfiguration Configuration { get; }

    /// <summary>
    /// Used for serialization
    /// </summary>
    IConfigurationXmlSerializer Serializer { get; }
    /// <summary>
    /// Saves the config
    /// </summary>
    void Save();
    /// <summary>
    /// Loads the config
    /// </summary>
    /// <returns></returns>
    IConfiguration Load();

    /// <summary>
    /// Creates the config from a viewModel
    /// </summary>
    /// <param name="viewModel"></param>
    void CreateFromViewModel(IConfigurationWindowViewModel viewModel);
  }
}