namespace TaskTimeTracker.Client.Contract.Configuration.Serialization {
  /// <summary>
  /// <para>I love this thingy</para>
  /// <para>Such generic, much wow!</para>
  /// </summary>
  /// <typeparam name="TEntity"></typeparam>
  /// <typeparam name="TSerializer"></typeparam>
  /// <typeparam name="TDeserializer"></typeparam>
  public interface ISerializer<TEntity, TSerializer, TDeserializer> {

    /// <summary>
    /// Serialize
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="configuration"></param>
    void Serialize(TSerializer writer, TEntity configuration);

    /// <summary>
    /// Deserialize
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    TEntity Deserialize(TDeserializer reader);
  }
}