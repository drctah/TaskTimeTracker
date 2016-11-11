using System;

namespace TaskTimeTracker.IO.Contract {
  public interface IVersionedReader<TEntity, TReader> {

    /// <summary>
    /// The Version this Reader can read
    /// </summary>
    Version MatchVersion { get; }

    /// <summary>
    /// Read the Entitty
    /// </summary>
    /// <returns></returns>
    TEntity Read(TReader reader);
  }
}