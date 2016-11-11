using System;

namespace TaskTimeTracker.IO.Contract {
  public interface IVersionedWriter<TEntity, TWriter> {

    Version MatchVersion { get; }

    void Write(TEntity entity, TWriter writer);
  }
}