using System;
using System.Collections.Generic;

namespace TaskTimeTracker.Client.Contract
{
  public interface ITaskTimeTrackerContractBaseObject : ICloneable, IComparable
  {
  }

  public interface ITaskTimeTrackerContractObject<T> : IComparable<ITaskTimeTrackerContractObject<T>>, IEquatable<ITaskTimeTrackerContractObject<T>>, ITaskTimeTrackerContractBaseObject
    where T : ITaskTimeTrackerContractBaseObject
  {
  }

  public interface ITaskTimeTrackerContractObjectCollection<TCollection, TElement> : ICollection<TElement>, ITaskTimeTrackerContractObject<TCollection>
    where TCollection : ITaskTimeTrackerContractBaseObject
    where TElement : ITaskTimeTrackerContractBaseObject
  {
    void AddRange(IEnumerable<TElement> items);
  }
}
