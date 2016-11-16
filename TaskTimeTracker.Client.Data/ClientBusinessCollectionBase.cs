using System;
using System.Collections;
using System.Collections.Generic;
using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client.Data
{
  public class ClientBusinessCollectionBase<TCollection, TElement> : ClientBusinessBase<TCollection>, ITaskTimeTrackerContractObjectCollection<TCollection, TElement>
    where TCollection : ITaskTimeTrackerContractObject<TCollection>
    where TElement : ITaskTimeTrackerContractObject<TElement>
  {
    private IList<ITaskTimeTrackerContractObject<TElement>> elements;

    public int Count { get { return this.elements.Count; } }

    public bool IsReadOnly { get { return false; } }

    public ClientBusinessCollectionBase()
    {
      this.elements = new List<ITaskTimeTrackerContractObject<TElement>>();
    }

    public void Add(ITaskTimeTrackerContractObject<TElement> item)
    {
      if (item == null) {
        throw new ArgumentNullException("item");
      }

      this.elements.Add(item);
    }

    public void AddRange(IEnumerable<ITaskTimeTrackerContractObject<TElement>> items)
    {
      if (items != null) {
        foreach (ITaskTimeTrackerContractObject<TElement> e in items) {
          if (e != null) {
            this.Add(e);
          }
        }
      }
    }

    public void Clear()
    {
      this.elements.Clear();
    }

    public bool Contains(ITaskTimeTrackerContractObject<TElement> item)
    {
      if (item == null) {
        return false;
      }

      return this.elements.Contains(item);
    }

    public void CopyTo(ITaskTimeTrackerContractObject<TElement>[] array, int arrayIndex)
    {
      this.elements.CopyTo(array, arrayIndex);
    }

    public IEnumerator<ITaskTimeTrackerContractObject<TElement>> GetEnumerator()
    {
      return this.elements.GetEnumerator();
    }

    public bool Remove(ITaskTimeTrackerContractObject<TElement> item)
    {
      if (item == null) {
        return false;
      }

      return this.elements.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.elements.GetEnumerator();
    }

    public override object Clone()
    {
      throw new NotImplementedException();
    }

    public override int CompareTo(ITaskTimeTrackerContractObject<TCollection> other)
    {
      throw new NotImplementedException();
    }
  }
}
