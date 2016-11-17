using System.Collections.Generic;

namespace Base.Configuration.Contract.Configuration {
  public interface ISectionedConfiguration : IConfiguration, ICollection<IConfigurationSection> {

  }
}