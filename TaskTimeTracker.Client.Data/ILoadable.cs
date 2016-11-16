using System;
using System.IO;

namespace TaskTimeTracker.Client.Data
{
  public interface ILoadable
  {
    void Load(string fileName);

    void Load(Stream stream);

    void Load(TextReader reader);
  }
}
