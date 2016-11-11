using System;

namespace TaskTimeTracker.Client.Contract.Configuration {
  public interface IConfiguration {
    Version Version { get; set; }
  }
}