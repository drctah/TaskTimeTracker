using System;
using System.Collections;
using System.Collections.Generic;

using TaskTimeTracker.Client.Contract;

namespace TaskTimeTracker.Client.Data
{
  public class TaskCollection : ITaskCollection
  {
    private IList<ITask> _elements;

    public TaskCollection()
    {
      this._elements = new List<ITask>();
    }

    public int Count { get { return this._elements.Count; } }

    public bool IsReadOnly { get { return false; } }

    public void Add(ITask item)
    {
      if (item == null) {
        throw new ArgumentNullException("item");
      }

      this._elements.Add(item);
    }

    public void Clear()
    {
      this._elements.Clear();
    }

    public bool Contains(ITask item)
    {
      if (item == null) {
        return false;
      }

      return this._elements.Contains(item);
    }

    public void CopyTo(ITask[] array, int arrayIndex)
    {
      this._elements.CopyTo(array, arrayIndex);
    }

    public bool Remove(ITask item)
    {
      if (item == null) {
        return false;
      }

      return this._elements.Remove(item);
    }

    public IEnumerator<ITask> GetEnumerator()
    {
      return this._elements.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this._elements.GetEnumerator();
    }
  }
}
