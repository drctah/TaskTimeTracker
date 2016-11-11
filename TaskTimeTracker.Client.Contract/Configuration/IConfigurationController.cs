using TaskTimeTracker.Client.Contract.Configuration.Serialization;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfigurationController {
    /// <summary>
    /// The Configuration
    /// </summary>
    ITaskTimeTrackerConfiguration Configuration { get; }

    /// <summary>
    /// Used for serialization
    /// </summary>
    IConfigurationXmlSerializer<ITaskTimeTrackerConfiguration> Serializer { get; }
    /// <summary>
    /// Saves the config
    /// </summary>
    void Save();
    /// <summary>
    /// Loads the config
    /// </summary>
    /// <returns></returns>
    ITaskTimeTrackerConfiguration Load();

    /// <summary>
    /// Creates the config from a viewModel
    /// </summary>
    /// <param name="viewModel"></param>
    void CreateFromViewModel(IConfigurationWindowViewModel viewModel);
  }
}