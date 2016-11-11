namespace TaskTimeTracker.IO.Contract {
  public interface IVersionedReader<TEntity, TReader> {

    TReader Reader { get; }

    TEntity Read();
  }
}