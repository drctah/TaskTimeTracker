namespace TaskTimeTracker.IO.Contract {
  public interface IVersionedWriter<TEntity, TWriter> {

    TWriter Writer { get; }

    void Write(TEntity entity);
  }
}