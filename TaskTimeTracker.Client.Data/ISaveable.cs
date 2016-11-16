using System;
using System.IO;

namespace TaskTimeTracker.Client.Data
{
  public interface ISaveable
  {
    void Save(string fileName);

    void Save(TextWriter writer);

    void Save(Stream stream);
  }
}
